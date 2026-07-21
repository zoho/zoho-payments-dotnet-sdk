using System;
using System.Linq;

namespace ZohoPayments.Params
{
    internal static class ParamValidator
    {
        // Throws if every value in fields is null.
        public static void RequireAnyField(params object?[] fields)
        {
            if (fields.All(field => field is null))
            {
                throw new ArgumentException("at least one field must be provided");
            }
        }


        public const int MaxDescriptionLength = 500;
        public const int MaxInvoiceNumberLength = 50;
        public const int MaxReferenceLength = 50;

        public static void ValidateDescription(string? description)
        {
            if (description != null && description.Length > MaxDescriptionLength)
            {
                throw new ArgumentException($"description must be at most {MaxDescriptionLength} characters");
            }
        }

        public static void ValidateInvoiceNumber(string? invoiceNumber)
        {
            if (invoiceNumber != null && invoiceNumber.Length > MaxInvoiceNumberLength)
            {
                throw new ArgumentException($"invoice_number must be at most {MaxInvoiceNumberLength} characters");
            }
        }

        public static void ValidateReferenceNumber(string? referenceNumber)
        {
            if (referenceNumber != null && referenceNumber.Length > MaxReferenceLength)
            {
                throw new ArgumentException($"reference number must be at most {MaxReferenceLength} characters");
            }
        }
    }
}
