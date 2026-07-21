using System;
using ZohoPayments.Internal;
using ZohoPayments.Models;
using ZohoPayments.Params;

namespace ZohoPayments.Services
{
    /// <summary>Zoho Payments Payments API (<c>/payments</c>).</summary>
    public sealed class PaymentService
    {
        private const string SingleEnvelope = "payment";
        private const string ListEnvelope = "payments";

        private readonly ZohoHttpClient _http;
        private readonly Edition _edition;

        internal PaymentService(ZohoHttpClient http, Edition edition)
        {
            _http = http;
            _edition = edition;
        }

        /// <summary>Requires <see cref="Edition.US"/>.</summary>
        public Payment Create(PaymentCreateParams parameters)
        {
            if (!_edition.IsUs())
            {
                throw new InvalidOperationException("payments.Create() is available only on Edition.US");
            }

            return _http.PostObject<Payment>("/payments", parameters, SingleEnvelope);
        }

        public Payment Get(string paymentId)
        {
            var path = $"/payments/{ZohoHttpClient.EncodePath(paymentId)}";
            return _http.GetObject<Payment>(path, SingleEnvelope);
        }

        public ListResponse<PaymentSummary> List(PaymentListParams? parameters = null)
        {
            var query = parameters?.ToQuery();
            return _http.ListObjects<PaymentSummary>("/payments", query, ListEnvelope);
        }
    }
}
