namespace ZohoPayments.Exceptions
{
    /// <summary>HTTP 429 - too many requests.</summary>
    public sealed class RateLimitException : ZohoPaymentsApiException
    {
        public RateLimitException(string? codeString, string? apiErrorMessage)
            : base(429, codeString, apiErrorMessage)
        {
        }
    }
}
