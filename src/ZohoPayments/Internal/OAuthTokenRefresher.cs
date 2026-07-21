using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using ZohoPayments.Auth;
using ZohoPayments.Exceptions;

namespace ZohoPayments.Internal
{
    // Exchanges a Zoho OAuth refresh token for a new access token.
    internal static class OAuthTokenRefresher
    {
        private const double ConnectTimeoutSeconds = 30.0;
        private const double RequestTimeoutSeconds = 60.0;
        private const int MaxErrorBodySnippet = 500;
        private const int DefaultExpiresIn = 3600;

        public static OAuthToken GenerateAccessToken(
            string refreshToken,
            string clientId,
            string clientSecret,
            string redirectUri,
            Edition edition)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new ArgumentException("refresh_token must not be null or empty", nameof(refreshToken));
            }

            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentException("client_id must not be null or empty", nameof(clientId));
            }

            if (string.IsNullOrEmpty(clientSecret))
            {
                throw new ArgumentException("client_secret must not be null or empty", nameof(clientSecret));
            }

            if (string.IsNullOrEmpty(redirectUri))
            {
                throw new ArgumentException("redirect_uri must not be null or empty", nameof(redirectUri));
            }

            var url = $"{edition.AccountsUrl()}/oauth/v2/token";
            var form = new Dictionary<string, string>
            {
                ["refresh_token"] = refreshToken,
                ["client_id"] = clientId,
                ["client_secret"] = clientSecret,
                ["redirect_uri"] = redirectUri,
                ["grant_type"] = "refresh_token"
            };

            string body;
            int statusCode;

            using (var handler = new HttpClientHandler { AllowAutoRedirect = false })
            using (var client = new HttpClient(handler) { Timeout = System.Threading.Timeout.InfiniteTimeSpan })
            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(ConnectTimeoutSeconds + RequestTimeoutSeconds)))
            {
                try
                {
                    var content = new FormUrlEncodedContent(form);
                    var response = client.PostAsync(url, content, cts.Token).ConfigureAwait(false).GetAwaiter().GetResult();
                    statusCode = (int)response.StatusCode;
                    body = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                }
                catch (OperationCanceledException exc)
                {
                    throw new ConnectionException("Token refresh timed out", exc);
                }
                catch (HttpRequestException exc)
                {
                    throw new ConnectionException($"Token refresh connection error: {exc.Message}", exc);
                }
            }

            var snippet = body.Length > MaxErrorBodySnippet ? body.Substring(0, MaxErrorBodySnippet) : body;

            if (statusCode < 200 || statusCode >= 300)
            {
                throw new ZohoPaymentsException($"Token refresh failed with HTTP {statusCode}: {snippet}");
            }

            JsonElement parsed;
            try
            {
                if (string.IsNullOrEmpty(body))
                {
                    parsed = default;
                }
                else
                {
                    using var document = JsonDocument.Parse(body);
                    parsed = document.RootElement.Clone();
                }
            }
            catch (JsonException exc)
            {
                throw new ZohoPaymentsException($"Token refresh response was not valid JSON: {snippet}", exc);
            }

            if (parsed.ValueKind != JsonValueKind.Object)
            {
                throw new ZohoPaymentsException($"Token refresh response missing 'access_token': {snippet}");
            }

            if (parsed.TryGetProperty("error", out _) && !parsed.TryGetProperty("access_token", out _))
            {
                var error = parsed.TryGetProperty("error", out var errorValue) ? errorValue.ToString() : "unknown";
                throw new ZohoPaymentsException($"Token refresh failed: {error}");
            }

            if (!parsed.TryGetProperty("access_token", out var accessTokenElement) ||
                accessTokenElement.ValueKind != JsonValueKind.String ||
                string.IsNullOrEmpty(accessTokenElement.GetString()))
            {
                throw new ZohoPaymentsException($"Token refresh response missing 'access_token': {snippet}");
            }

            var accessToken = accessTokenElement.GetString()!;

            var expiresIn = DefaultExpiresIn;
            if (parsed.TryGetProperty("expires_in_sec", out var expiresInSec) && expiresInSec.ValueKind == JsonValueKind.Number)
            {
                expiresIn = expiresInSec.GetInt32();
            }
            else if (parsed.TryGetProperty("expires_in", out var expiresInRaw) && expiresInRaw.ValueKind == JsonValueKind.Number)
            {
                expiresIn = expiresInRaw.GetInt32();
            }

            return new OAuthToken(accessToken, expiresIn);
        }
    }
}
