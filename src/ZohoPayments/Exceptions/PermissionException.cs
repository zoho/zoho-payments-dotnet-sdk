namespace ZohoPayments.Exceptions
{
    /// <summary>HTTP 403 - insufficient permissions.</summary>
    public sealed class PermissionException : ZohoPaymentsApiException
    {
        public PermissionException(string? codeString, string? apiErrorMessage)
            : base(403, codeString, apiErrorMessage)
        {
        }
    }
}
