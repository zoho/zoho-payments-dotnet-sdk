using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using ZohoPayments.Exceptions;

namespace ZohoPayments.Net
{
    /// <summary>Default HTTP transport, backed by <see cref="System.Net.Http.HttpClient"/>.</summary>
    public sealed class DefaultHttpClient : IHttpClient
    {
        private readonly HttpClient _client;
        private readonly double _defaultTimeoutSeconds;
        private volatile bool _closed;
        private readonly object _closeLock = new object();

        public DefaultHttpClient(double connectTimeoutSeconds, double defaultTimeoutSeconds)
        {
            _defaultTimeoutSeconds = defaultTimeoutSeconds;

#if NET5_0_OR_GREATER
            var handler = new SocketsHttpHandler
            {
                ConnectTimeout = TimeSpan.FromSeconds(connectTimeoutSeconds),
                AllowAutoRedirect = false
            };
            _client = new HttpClient(handler);
#else
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };
            _client = new HttpClient(handler);
#endif
            // Overall per-call timeout is enforced per-request below via CancellationToken,
            // so disable the ambient HttpClient.Timeout (which would otherwise apply globally).
            _client.Timeout = System.Threading.Timeout.InfiniteTimeSpan;
        }

        public ZohoResponse Execute(ZohoRequest request)
        {
            if (_closed)
            {
                throw new ConnectionException("HTTP client has been closed");
            }

            var httpRequest = new HttpRequestMessage(ToHttpMethod(request.Method), request.Url);

            // Flatten multi-valued headers to comma-joined strings (RFC 7230 section 3.2.2).
            foreach (var header in request.Headers)
            {
                var value = string.Join(", ", header.Value);
                if (string.Equals(header.Key, "Content-Type", StringComparison.OrdinalIgnoreCase))
                {
                    continue; // set on the content below
                }

                httpRequest.Headers.TryAddWithoutValidation(header.Key, value);
            }

            if (request.Body != null)
            {
                var content = new StringContent(request.Body, Encoding.UTF8);
                content.Headers.ContentType = null;
                if (request.Headers.TryGetValue("Content-Type", out var contentType))
                {
                    content.Headers.TryAddWithoutValidation("Content-Type", string.Join(", ", contentType));
                }

                httpRequest.Content = content;
            }

            var timeoutSeconds = request.Timeout ?? _defaultTimeoutSeconds;
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds));

            try
            {
                using var response = _client.SendAsync(httpRequest, HttpCompletionOption.ResponseContentRead, cts.Token)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();

                var body = response.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();

                var responseHeaders = new Dictionary<string, List<string>>();
                foreach (var header in response.Headers)
                {
                    responseHeaders[header.Key] = header.Value.ToList();
                }

                foreach (var header in response.Content.Headers)
                {
                    responseHeaders[header.Key] = header.Value.ToList();
                }

                return new ZohoResponse((int)response.StatusCode, responseHeaders, string.IsNullOrEmpty(body) ? null : body);
            }
            catch (OperationCanceledException exc)
            {
                throw new ConnectionException("Request timed out", exc);
            }
            catch (HttpRequestException exc)
            {
                throw new ConnectionException($"Connection error: {exc.Message}", exc);
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

            _client.Dispose();
        }

        private static HttpMethod ToHttpMethod(RequestMethod method)
        {
            switch (method)
            {
                case RequestMethod.GET:
                    return HttpMethod.Get;
                case RequestMethod.POST:
                    return HttpMethod.Post;
                case RequestMethod.PUT:
                    return HttpMethod.Put;
                case RequestMethod.DELETE:
                    return HttpMethod.Delete;
                default:
                    throw new ArgumentOutOfRangeException(nameof(method), method, "unknown request method");
            }
        }
    }
}
