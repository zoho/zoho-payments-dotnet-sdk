using ZohoPayments.Internal;
using ZohoPayments.Models;
using ZohoPayments.Params;

namespace ZohoPayments.Services
{
    /// <summary>Payment Method Session API (<c>/paymentmethodsessions</c>). Requires <see cref="Edition.US"/>.</summary>
    public sealed class PaymentMethodSessionService
    {
        private const string Envelope = "payment_method_session";

        private readonly ZohoHttpClient _http;

        internal PaymentMethodSessionService(ZohoHttpClient http)
        {
            _http = http;
        }

        public PaymentMethodSession Create(PaymentMethodSessionCreateParams parameters) =>
            _http.PostObject<PaymentMethodSession>("/paymentmethodsessions", parameters, Envelope);

        public PaymentMethodSession Get(string paymentMethodSessionId)
        {
            var path = $"/paymentmethodsessions/{ZohoHttpClient.EncodePath(paymentMethodSessionId)}";
            return _http.GetObject<PaymentMethodSession>(path, Envelope);
        }
    }
}
