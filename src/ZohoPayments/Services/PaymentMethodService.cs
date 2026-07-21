using ZohoPayments.Internal;
using ZohoPayments.Models;
using ZohoPayments.Params;

namespace ZohoPayments.Services
{
    /// <summary>Payment Methods API (<c>/paymentmethods</c>) — retrieve, update, or delete a saved method. Requires <see cref="Edition.US"/>.</summary>
    public sealed class PaymentMethodService
    {
        private const string Envelope = "payment_method";

        private readonly ZohoHttpClient _http;

        internal PaymentMethodService(ZohoHttpClient http)
        {
            _http = http;
        }

        public PaymentMethod Get(string paymentMethodId)
        {
            var path = $"/paymentmethods/{ZohoHttpClient.EncodePath(paymentMethodId)}";
            return _http.GetObject<PaymentMethod>(path, Envelope);
        }

        public PaymentMethod Update(string paymentMethodId, PaymentMethodUpdateParams parameters)
        {
            var path = $"/paymentmethods/{ZohoHttpClient.EncodePath(paymentMethodId)}";
            return _http.PutObject<PaymentMethod>(path, parameters, Envelope);
        }

        public void Delete(string paymentMethodId)
        {
            var path = $"/paymentmethods/{ZohoHttpClient.EncodePath(paymentMethodId)}";
            _http.Delete(path);
        }
    }
}
