using ZohoPayments.Internal;
using ZohoPayments.Models;
using ZohoPayments.Params;

namespace ZohoPayments.Services
{
    /// <summary>Zoho Payments Payment Links API (<c>/paymentlinks</c>).</summary>
    public sealed class PaymentLinkService
    {
        private const string Envelope = "payment_links";

        private readonly ZohoHttpClient _http;

        internal PaymentLinkService(ZohoHttpClient http)
        {
            _http = http;
        }

        public PaymentLink Create(PaymentLinkCreateParams parameters) =>
            _http.PostObject<PaymentLink>("/paymentlinks", parameters, Envelope);

        public PaymentLink Get(string paymentLinkId)
        {
            var path = $"/paymentlinks/{ZohoHttpClient.EncodePath(paymentLinkId)}";
            return _http.GetObject<PaymentLink>(path, Envelope);
        }

        public PaymentLink Update(string paymentLinkId, PaymentLinkUpdateParams parameters)
        {
            var path = $"/paymentlinks/{ZohoHttpClient.EncodePath(paymentLinkId)}";
            return _http.PutObject<PaymentLink>(path, parameters, Envelope);
        }

        public PaymentLink Cancel(string paymentLinkId)
        {
            var path = $"/paymentlinks/{ZohoHttpClient.EncodePath(paymentLinkId)}/cancel";
            return _http.PutObject<PaymentLink>(path, null, Envelope);
        }
    }
}
