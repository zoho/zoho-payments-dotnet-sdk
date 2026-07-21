using System;
using System.Collections.Generic;
using System.Text.Json;
using ZohoPayments.Exceptions;
using ZohoPayments.Models;
using ZohoPayments.Net;

namespace ZohoPayments.Internal
{
    // Central HTTP client: authenticated request dispatch, envelope handling, and error mapping.
    internal sealed class ZohoHttpClient
    {
        private static readonly string UserAgent = $"{SdkVersionInfo.SdkName}/{SdkVersionInfo.SdkVersion}";

        private readonly IHttpClient _transport;
        private readonly TokenManager _tokenManager;
        private readonly Edition _edition;
        private readonly string _accountId;
        private readonly double? _requestTimeout;
        private readonly IReadOnlyDictionary<string, string> _defaultHeaders;
        private volatile bool _closed;
        private readonly object _closeLock = new object();

        public ZohoHttpClient(
            IHttpClient transport,
            TokenManager tokenManager,
            Edition edition,
            string accountId,
            double? requestTimeout,
            IReadOnlyDictionary<string, string> defaultHeaders)
        {
            _transport = transport;
            _tokenManager = tokenManager;
            _edition = edition;
            _accountId = accountId;
            _requestTimeout = requestTimeout;
            _defaultHeaders = defaultHeaders;
        }

        public static string EncodePath(string segment)
        {
            if (segment is null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            return Uri.EscapeDataString(segment);
        }

        public ApiResponse Get(string path, QueryParams? query = null) => Request(RequestMethod.GET, path, query, null);

        public ApiResponse Post(string path, object? body) => Request(RequestMethod.POST, path, null, body);

        public ApiResponse Put(string path, object? body = null) => Request(RequestMethod.PUT, path, null, body);

        public ApiResponse Delete(string path) => Request(RequestMethod.DELETE, path, null, null);

        public ApiResponse Request(RequestMethod method, string path, QueryParams? query, object? body)
        {
            if (_closed)
            {
                throw new ZohoPaymentsException("HTTP client has been closed");
            }

            var url = BuildUrl(path, query);

            var builder = ZohoRequest.Builder().Method(method).Url(url);

            // User-provided default headers first (lower priority).
            foreach (var header in _defaultHeaders)
            {
                builder.SetHeader(header.Key, header.Value);
            }

            // SDK-managed headers override user defaults.
            builder.SetHeader("Authorization", $"Zoho-oauthtoken {_tokenManager.GetAccessToken()}");
            builder.SetHeader("User-Agent", UserAgent);
            builder.SetHeader("Accept", "application/json");

            if (body != null)
            {
                var bodyJson = JsonUtil.ToJson(body);
                builder.SetHeader("Content-Type", "application/json");
                builder.Body(bodyJson);
            }

            if (_requestTimeout.HasValue)
            {
                builder.Timeout(_requestTimeout.Value);
            }

            ZohoResponse response;
            try
            {
                response = _transport.Execute(builder.Build());
            }
            catch (ConnectionException)
            {
                throw;
            }
            catch (Exception exc)
            {
                throw new ConnectionException($"Transport failure: {exc.Message}", exc);
            }

            JsonElement? parsed = null;
            if (!string.IsNullOrEmpty(response.Body))
            {
                try
                {
                    parsed = JsonUtil.ParseObject(response.Body!);
                }
                catch (ZohoPaymentsException)
                {
                    // Non-JSON body on an error response - treat as an empty object.
                    parsed = null;
                }
            }

            var apiResponse = new ApiResponse(response.StatusCode, parsed);

            if (!apiResponse.IsSuccess())
            {
                RaiseForStatus(apiResponse);
            }

            return apiResponse;
        }

        public T GetObject<T>(string path, params string[] envelopeKeys) => JsonUtil.Unwrap<T>(Get(path).Body, envelopeKeys);

        public T PostObject<T>(string path, object? body, params string[] envelopeKeys) =>
            JsonUtil.Unwrap<T>(Post(path, body).Body, envelopeKeys);

        public T PutObject<T>(string path, object? body, params string[] envelopeKeys) =>
            JsonUtil.Unwrap<T>(Put(path, body).Body, envelopeKeys);

        public ListResponse<T> ListObjects<T>(string path, QueryParams? query, params string[] envelopeKeys)
        {
            var response = Get(path, query);
            var entries = JsonUtil.ListFromBody(response.Body, envelopeKeys);
            var items = new List<T>();
            foreach (var entry in entries)
            {
                var item = entry.Deserialize<T>(JsonUtil.DeserializeOptions);
                if (item != null)
                {
                    items.Add(item);
                }
            }

            var pageContext = JsonUtil.ReadPageContext(response.Body);
            return new ListResponse<T>(items, pageContext);
        }

        private string BuildUrl(string path, QueryParams? query)
        {
            var basePath = _edition.BaseUrl();
            if (!path.StartsWith("/", StringComparison.Ordinal))
            {
                path = "/" + path;
            }

            var queryParams = new QueryParams();
            queryParams.AddAll(query);
            queryParams.Add("account_id", _accountId);

            var url = basePath + path;
            if (!queryParams.IsEmpty)
            {
                url = $"{url}?{queryParams.ToQueryString()}";
            }

            return url;
        }

        private static void RaiseForStatus(ApiResponse apiResponse)
        {
            var status = apiResponse.StatusCode;
            var codeString = apiResponse.GetCodeString();
            var message = apiResponse.GetMessage();

            switch (status)
            {
                case 400:
                case 422:
                    throw new InvalidRequestException(status, codeString, message);
                case 401:
                    throw new AuthenticationException(codeString, message);
                case 403:
                    throw new PermissionException(codeString, message);
                case 404:
                    throw new ResourceNotFoundException(codeString, message);
                case 429:
                    throw new RateLimitException(codeString, message);
                default:
                    throw new ZohoPaymentsApiException(status, codeString, message);
            }
        }

        public void Close()
        {
            lock (_closeLock)
            {
                if (_closed)
                {
                    return;
                }

                _closed = true;
            }

            try
            {
                _transport.Close();
            }
            catch
            {
                // ignored - best-effort cleanup
            }
        }
    }
}
