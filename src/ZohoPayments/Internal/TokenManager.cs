using System;

namespace ZohoPayments.Internal
{
    // Stores the current OAuth access token with thread-safe updates.
    internal sealed class TokenManager
    {
        private volatile string _accessToken;

        public TokenManager(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("access_token must not be null or empty", nameof(accessToken));
            }

            _accessToken = accessToken;
        }

        public string GetAccessToken() => _accessToken;

        public void UpdateToken(string newAccessToken)
        {
            if (string.IsNullOrEmpty(newAccessToken))
            {
                throw new ArgumentException("new_access_token must not be null or empty", nameof(newAccessToken));
            }

            _accessToken = newAccessToken;
        }

        public override string ToString() => "TokenManager{[REDACTED]}";
    }
}
