namespace ZohoPayments.Exceptions
{
    /// <summary>HTTP 401 - invalid or expired OAuth token.</summary>
    public sealed class AuthenticationException : ZohoPaymentsApiException
    {
        public AuthenticationException(string? codeString, string? apiErrorMessage)
            : base(401, codeString, apiErrorMessage)
        {
        }
    }
}
