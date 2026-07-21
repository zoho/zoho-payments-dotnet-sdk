using ZohoPayments.Internal;
using ZohoPayments.Models;
using ZohoPayments.Params;

namespace ZohoPayments.Services
{
    /// <summary>Zoho Payments Collect API — virtual accounts (<c>/virtualaccounts</c>). Requires <see cref="Edition.IN"/>.</summary>
    public sealed class CollectService
    {
        private const string SingleEnvelope = "virtual_account";
        private const string PaymentsEnvelope = "payments";

        private readonly ZohoHttpClient _http;

        internal CollectService(ZohoHttpClient http)
        {
            _http = http;
        }

        public VirtualAccount Create(VirtualAccountCreateParams parameters) =>
            _http.PostObject<VirtualAccount>("/virtualaccounts", parameters, SingleEnvelope);

        public VirtualAccount Update(string virtualAccountId, VirtualAccountUpdateParams parameters)
        {
            var path = $"/virtualaccounts/{ZohoHttpClient.EncodePath(virtualAccountId)}";
            return _http.PutObject<VirtualAccount>(path, parameters, SingleEnvelope);
        }

        public VirtualAccount Get(string virtualAccountId)
        {
            var path = $"/virtualaccounts/{ZohoHttpClient.EncodePath(virtualAccountId)}";
            return _http.GetObject<VirtualAccount>(path, SingleEnvelope);
        }

        public ListResponse<VirtualAccountPayment> ListPayments(string virtualAccountId, VirtualAccountPaymentListParams? parameters = null)
        {
            var path = $"/virtualaccounts/{ZohoHttpClient.EncodePath(virtualAccountId)}/payments";
            var query = parameters?.ToQuery();
            return _http.ListObjects<VirtualAccountPayment>(path, query, PaymentsEnvelope);
        }

        public void Close(string virtualAccountId)
        {
            var path = $"/virtualaccounts/{ZohoHttpClient.EncodePath(virtualAccountId)}/close";
            _http.Put(path, null);
        }
    }
}
