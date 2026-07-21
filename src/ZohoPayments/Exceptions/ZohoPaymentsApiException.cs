namespace ZohoPayments.Exceptions
{
    /// <summary>Base for HTTP non-2xx API responses.</summary>
    public class ZohoPaymentsApiException : ZohoPaymentsException
    {
        public ZohoPaymentsApiException(int httpStatusCode, string? codeString, string? apiErrorMessage)
            : base($"API error (HTTP {httpStatusCode}): code={codeString}, message={apiErrorMessage}")
        {
            HttpStatusCode = httpStatusCode;
            CodeString = codeString;
            ApiErrorMessage = apiErrorMessage;
        }

        public int HttpStatusCode { get; }

        public string? CodeString { get; }

        public string? ApiErrorMessage { get; }
    }
}
