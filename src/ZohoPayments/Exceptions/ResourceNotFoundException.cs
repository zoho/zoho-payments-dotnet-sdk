namespace ZohoPayments.Exceptions
{
    /// <summary>HTTP 404 - resource not found.</summary>
    public sealed class ResourceNotFoundException : ZohoPaymentsApiException
    {
        public ResourceNotFoundException(string? codeString, string? apiErrorMessage)
            : base(404, codeString, apiErrorMessage)
        {
        }
    }
}
