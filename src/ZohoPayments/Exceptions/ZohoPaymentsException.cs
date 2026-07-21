using System;

namespace ZohoPayments.Exceptions
{
    /// <summary>Base exception for all Zoho Payments SDK errors.</summary>
    public class ZohoPaymentsException : Exception
    {
        public ZohoPaymentsException(string message) : base(message)
        {
        }

        public ZohoPaymentsException(string message, Exception? cause) : base(message, cause)
        {
        }
    }
}
