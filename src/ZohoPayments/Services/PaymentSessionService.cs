using ZohoPayments.Internal;
using ZohoPayments.Models;
using ZohoPayments.Params;

namespace ZohoPayments.Services
{
    /// <summary>Zoho Payments Payment Session API (<c>/paymentsessions</c>).</summary>
    public sealed class PaymentSessionService
    {
        private const string Envelope = "payments_session";

        private readonly ZohoHttpClient _http;

        internal PaymentSessionService(ZohoHttpClient http)
        {
            _http = http;
        }

        public PaymentSession Create(PaymentSessionCreateParams parameters) =>
            _http.PostObject<PaymentSession>("/paymentsessions", parameters, Envelope);

        public PaymentSession Get(string paymentSessionId)
        {
            var path = $"/paymentsessions/{ZohoHttpClient.EncodePath(paymentSessionId)}";
            return _http.GetObject<PaymentSession>(path, Envelope);
        }
    }
}
