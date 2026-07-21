namespace ZohoPayments.Internal
{
    // Masks the OAuth access token when SDK types that carry it are rendered via ToString().
    internal static class Redact
    {
        public const string Mask = "[REDACTED]";

        // Empty string for an empty secret, otherwise the mask placeholder.
        public static string Token(string? secret) => string.IsNullOrEmpty(secret) ? string.Empty : Mask;

        // Masks the value when the header is Authorization, otherwise returns it unchanged.
        public static string MaskHeaderValue(string name, string value) =>
            string.Equals(name, "Authorization", System.StringComparison.OrdinalIgnoreCase) ? Mask : value;
    }
}
