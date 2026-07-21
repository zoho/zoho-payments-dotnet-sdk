namespace ZohoPayments.Exceptions
{
    /// <summary>HTTP 400 or 422 - malformed request or validation error.</summary>
    public sealed class InvalidRequestException : ZohoPaymentsApiException
    {
        public InvalidRequestException(int httpStatusCode, string? codeString, string? apiErrorMessage)
            : base(httpStatusCode, codeString, apiErrorMessage)
        {
        }
    }
}
