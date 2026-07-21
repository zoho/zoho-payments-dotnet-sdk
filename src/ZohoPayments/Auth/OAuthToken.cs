using System;
using ZohoPayments.Internal;

namespace ZohoPayments.Auth
{
    /// <summary>An OAuth access token returned by <see cref="ZohoPaymentsClient.GenerateAccessToken"/>.</summary>
    public sealed class OAuthToken
    {
        public OAuthToken(string accessToken, int expiresIn)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("access_token must not be null or empty", nameof(accessToken));
            }

            AccessToken = accessToken;
            ExpiresIn = expiresIn;
        }

        public string AccessToken { get; }

        public int ExpiresIn { get; }

        /// <summary>Masks the access token so it is never leaked via logging or string interpolation.</summary>
        public override string ToString() => $"OAuthToken{{AccessToken: {Redact.Token(AccessToken)}, ExpiresIn: {ExpiresIn}}}";
    }
}
