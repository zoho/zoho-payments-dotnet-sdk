using ZohoPayments.Internal;
using ZohoPayments.Models;
using ZohoPayments.Params;

namespace ZohoPayments.Services
{
    /// <summary>Zoho Payments Refunds API (<c>/refunds</c>).</summary>
    public sealed class RefundService
    {
        private const string Envelope = "refund";

        private readonly ZohoHttpClient _http;

        internal RefundService(ZohoHttpClient http)
        {
            _http = http;
        }

        public Refund Create(string paymentId, RefundCreateParams parameters)
        {
            var path = $"/payments/{ZohoHttpClient.EncodePath(paymentId)}/refunds";
            return _http.PostObject<Refund>(path, parameters, Envelope);
        }

        public Refund Get(string refundId)
        {
            var path = $"/refunds/{ZohoHttpClient.EncodePath(refundId)}";
            return _http.GetObject<Refund>(path, Envelope);
        }
    }
}
