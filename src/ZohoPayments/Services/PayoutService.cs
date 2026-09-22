using System;
using ZohoPayments.Internal;
using ZohoPayments.Models;
using ZohoPayments.Params;

namespace ZohoPayments.Services
{
    /// <summary>Zoho Payments Payouts API (<c>/payouts</c>).</summary>
    public sealed class PayoutService
    {
        private const string SingleEnvelope = "payout";
        private const string ListEnvelope = "payouts";
        private const string TransactionsEnvelope = "transactions";

        private readonly ZohoHttpClient _http;

        internal PayoutService(ZohoHttpClient http)
        {
            _http = http;
        }

        public PayoutDetail Get(string payoutId)
        {
            RequirePayoutId(payoutId);
            var path = $"/payouts/{ZohoHttpClient.EncodePath(payoutId)}";
            return _http.GetObject<PayoutDetail>(path, SingleEnvelope);
        }

        public ListResponse<Payout> List(PayoutListParams? parameters = null)
        {
            var query = parameters?.ToQuery();
            return _http.ListObjects<Payout>("/payouts", query, ListEnvelope);
        }

        public ListResponse<PayoutTransaction> ListTransactions(
            string payoutId,
            PayoutTransactionListParams? parameters = null)
        {
            RequirePayoutId(payoutId);
            var path = $"/payouts/{ZohoHttpClient.EncodePath(payoutId)}/transactions";
            var query = parameters?.ToQuery();
            return _http.ListObjects<PayoutTransaction>(path, query, TransactionsEnvelope);
        }

        private static void RequirePayoutId(string payoutId)
        {
            if (string.IsNullOrEmpty(payoutId))
            {
                throw new ArgumentException("payoutId is required", nameof(payoutId));
            }
        }
    }
}
