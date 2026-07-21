using System;

namespace ZohoPayments
{
    /// <summary>Target Zoho Payments edition. Determines the API base URL and which resources are available.</summary>
    public enum Edition
    {
        IN,
        IN_SANDBOX,
        US
    }

    public static class EditionExtensions
    {
        public static string BaseUrl(this Edition edition)
        {
            switch (edition)
            {
                case Edition.IN:
                    return "https://payments.zoho.in/api/v1";
                case Edition.IN_SANDBOX:
                    return "https://paymentssandbox.zoho.in/api/v1";
                case Edition.US:
                    return "https://payments.zoho.com/api/v1";
                default:
                    throw new ArgumentOutOfRangeException(nameof(edition), edition, "unknown edition");
            }
        }

        public static string AccountsUrl(this Edition edition)
        {
            switch (edition)
            {
                case Edition.IN:
                case Edition.IN_SANDBOX:
                    return "https://accounts.zoho.in";
                case Edition.US:
                    return "https://accounts.zoho.com";
                default:
                    throw new ArgumentOutOfRangeException(nameof(edition), edition, "unknown edition");
            }
        }

        public static bool IsUs(this Edition edition) => edition == Edition.US;

        public static bool IsIn(this Edition edition) =>
            edition == Edition.IN || edition == Edition.IN_SANDBOX;

        public static Edition FromString(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("edition name must not be null or empty", nameof(name));
            }

            if (Enum.TryParse<Edition>(name.ToUpperInvariant(), out var edition))
            {
                return edition;
            }

            throw new ArgumentException(
                $"unknown edition: '{name}'. Expected one of: {string.Join(", ", Enum.GetNames(typeof(Edition)))}",
                nameof(name));
        }
    }
}
