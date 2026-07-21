namespace ZohoPayments.Net
{
    /// <summary>Pluggable HTTP transport: turns a <see cref="ZohoRequest"/> into a <see cref="ZohoResponse"/>; throw <see cref="ZohoPayments.Exceptions.ConnectionException"/> on network failure.</summary>
    public interface IHttpClient
    {
        ZohoResponse Execute(ZohoRequest request);

        /// <summary>Releases any transport-level resources. Safe to call multiple times.</summary>
        void Close();
    }
}
