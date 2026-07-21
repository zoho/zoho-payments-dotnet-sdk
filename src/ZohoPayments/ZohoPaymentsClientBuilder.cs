using System;
using System.Collections.Generic;
using ZohoPayments.Auth;
using ZohoPayments.Internal;
using ZohoPayments.Net;

namespace ZohoPayments
{
    public sealed class ZohoPaymentsClientBuilder
    {
        public const double DefaultConnectTimeout = Defaults.DefaultConnectTimeout;
        public const double DefaultRequestTimeout = Defaults.DefaultRequestTimeout;

        private static readonly HashSet<string> ReservedHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "authorization",
            "user-agent",
            "accept",
            "content-type",
            "content-length",
            "host"
        };

        private string? _accountId;
        private global::ZohoPayments.Edition? _edition;
        private string? _accessToken;
        private IHttpClient? _httpClient;
        private double? _connectTimeout;
        private double? _requestTimeout;
        private readonly Dictionary<string, string> _defaultHeaders = new Dictionary<string, string>();
        private bool _consumed;

        internal ZohoPaymentsClientBuilder()
        {
        }

        /// <summary>Zoho account ID for which the SDK makes API calls. Required.</summary>
        public ZohoPaymentsClientBuilder AccountId(string accountId)
        {
            _accountId = accountId;
            return this;
        }

        /// <summary>Target edition: <see cref="Edition.IN"/>, <see cref="Edition.IN_SANDBOX"/>, or <see cref="Edition.US"/>. Required.</summary>
        public ZohoPaymentsClientBuilder Edition(global::ZohoPayments.Edition edition)
        {
            _edition = edition;
            return this;
        }

        /// <summary>Sets the OAuth access token from a raw string.</summary>
        public ZohoPaymentsClientBuilder OauthToken(string token)
        {
            _accessToken = token;
            return this;
        }

        /// <summary>Sets the OAuth access token from an <see cref="OAuthToken"/>.</summary>
        public ZohoPaymentsClientBuilder OauthToken(OAuthToken token)
        {
            if (token is null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            _accessToken = token.AccessToken;
            return this;
        }

        /// <summary>Supplies a custom HTTP transport. Cannot be combined with <see cref="ConnectTimeout"/> - a custom transport controls its own timeouts.</summary>
        public ZohoPaymentsClientBuilder HttpClient(IHttpClient httpClient)
        {
            _httpClient = httpClient;
            return this;
        }

        /// <summary>TCP connect timeout for the default transport, in seconds. Default: 30.</summary>
        public ZohoPaymentsClientBuilder ConnectTimeout(double seconds)
        {
            _connectTimeout = seconds;
            return this;
        }

        /// <summary>Per-request read timeout, in seconds. Default: 60.</summary>
        public ZohoPaymentsClientBuilder RequestTimeout(double seconds)
        {
            _requestTimeout = seconds;
            return this;
        }

        /// <summary>
        /// Adds a default header sent with every request. SDK-managed headers (Authorization, User-Agent,
        /// Accept, Content-Type, Content-Length, Host) are rejected.
        /// </summary>
        public ZohoPaymentsClientBuilder AddDefaultHeader(string name, string value)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("header name must not be null or empty", nameof(name));
            }

            if (ReservedHeaders.Contains(name))
            {
                throw new ArgumentException($"header '{name}' is managed by the SDK and cannot be overridden", nameof(name));
            }

            _defaultHeaders[name] = value;
            return this;
        }

        /// <summary>Builds a <see cref="ZohoPaymentsClient"/>. The builder is single-use; further calls throw <see cref="InvalidOperationException"/>.</summary>
        public ZohoPaymentsClient Build()
        {
            if (_consumed)
            {
                throw new InvalidOperationException("Builder has already been consumed");
            }

            _consumed = true;

            if (string.IsNullOrEmpty(_accountId))
            {
                throw new InvalidOperationException("account_id is required");
            }

            if (_edition is null)
            {
                throw new InvalidOperationException("edition is required");
            }

            if (string.IsNullOrEmpty(_accessToken))
            {
                throw new InvalidOperationException("oauth_token is required");
            }

            if (_httpClient != null && _connectTimeout.HasValue)
            {
                throw new InvalidOperationException("connect_timeout and a custom http_client are mutually exclusive");
            }

            IHttpClient transport;
            if (_httpClient != null)
            {
                transport = _httpClient;
            }
            else
            {
                var connectTimeout = _connectTimeout ?? DefaultConnectTimeout;
                var defaultRequestTimeout = _requestTimeout ?? DefaultRequestTimeout;
                transport = new DefaultHttpClient(connectTimeout, defaultRequestTimeout);
            }

            var tokenManager = new TokenManager(_accessToken!);
            var httpClient = new ZohoHttpClient(
                transport,
                tokenManager,
                _edition.Value,
                _accountId!,
                _requestTimeout,
                _defaultHeaders);

            return new ZohoPaymentsClient(httpClient, tokenManager, _edition.Value);
        }

        /// <summary>Renders the builder with the OAuth token masked so it is never leaked via logging.</summary>
        public override string ToString() =>
            $"ZohoPaymentsClientBuilder{{AccountId: {_accountId}, Edition: {_edition}, OauthToken: {Redact.Token(_accessToken)}}}";
    }
}
