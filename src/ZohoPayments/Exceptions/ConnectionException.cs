using System;

namespace ZohoPayments.Exceptions
{
    /// <summary>Network/IO failure while executing a request.</summary>
    public sealed class ConnectionException : ZohoPaymentsException
    {
        public ConnectionException(string message) : base(message)
        {
        }

        public ConnectionException(string message, Exception? cause) : base(message, cause)
        {
        }
    }
}
