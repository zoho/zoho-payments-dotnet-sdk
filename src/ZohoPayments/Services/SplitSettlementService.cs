using System;
using ZohoPayments.Internal;
using ZohoPayments.Models;
using ZohoPayments.Params;

namespace ZohoPayments.Services
{
    /// <summary>
    /// Zoho Payments Split Settlement APIs — transfers (<c>/transfers</c>), transfer reversals
    /// (<c>/transferreversals</c>) and connected accounts (<c>/connectedaccounts</c>).
    /// Requires <see cref="Edition.IN"/>.
    /// </summary>
    public sealed class SplitSettlementService
    {
        private const string DataEnvelope = "data";
        private const string TransferEnvelope = "transfer_details";
        private const string TransfersEnvelope = "transfers";
        private const string TransferReversalEnvelope = "transfer_reversal_details";
        private const string TransferReversalsEnvelope = "transfer_reversals";
        private const string TransferReversalCreateEnvelope = "transfer_reversal";
        private const string ConnectedAccountEnvelope = "connected_account";
        private const string ConnectedAccountsEnvelope = "connected_accounts";
        private const string PayoutEnvelope = "payout";
        private const string PayoutsEnvelope = "payouts";
        private const string TransactionsEnvelope = "transactions";

        private readonly ZohoHttpClient _http;

        internal SplitSettlementService(ZohoHttpClient http)
        {
            _http = http;
        }

        // Transfers

        /// <summary>
        /// Splits a payment across connected accounts. A request may partially succeed — inspect
        /// each entry of <see cref="TransferCreateResponse.Splits"/> for its own status.
        /// </summary>
        public TransferCreateResponse CreateTransfer(TransferCreateParams parameters)
        {
            RequireParams(parameters);
            return _http.PostObject<TransferCreateResponse>(
                "/transfers", parameters, DataEnvelope, TransfersEnvelope);
        }

        public Transfer GetTransfer(string transferId)
        {
            Require(transferId, "transferId", nameof(transferId));
            var path = $"/transfers/{ZohoHttpClient.EncodePath(transferId)}";
            return _http.GetObject<Transfer>(path, TransferEnvelope);
        }

        public ListResponse<TransferSummary> ListTransfers(TransferListParams? parameters = null)
        {
            var query = parameters?.ToQuery();
            return _http.ListObjects<TransferSummary>("/transfers", query, TransfersEnvelope);
        }

        // Transfer reversals

        public TransferReversalCreateResponse CreateTransferReversal(TransferReversalCreateParams parameters)
        {
            RequireParams(parameters);
            return _http.PostObject<TransferReversalCreateResponse>(
                "/transferreversals", parameters, DataEnvelope, TransferReversalCreateEnvelope);
        }

        public TransferReversalDetail GetTransferReversal(string transferReversalId)
        {
            Require(transferReversalId, "transferReversalId", nameof(transferReversalId));
            var path = $"/transferreversals/{ZohoHttpClient.EncodePath(transferReversalId)}";
            return _http.GetObject<TransferReversalDetail>(path, TransferReversalEnvelope);
        }

        public ListResponse<TransferReversal> ListTransferReversals(
            TransferReversalListParams? parameters = null)
        {
            var query = parameters?.ToQuery();
            return _http.ListObjects<TransferReversal>("/transferreversals", query, TransferReversalsEnvelope);
        }

        // Connected accounts

        public void CreateConnectedAccount(ConnectedAccountCreateParams parameters)
        {
            RequireParams(parameters);
            _http.Post("/connectedaccounts", parameters);
        }

        public ConnectedAccount GetConnectedAccount(string connectedAccountId)
        {
            RequireConnectedAccountId(connectedAccountId);
            var path = $"/connectedaccounts/{ZohoHttpClient.EncodePath(connectedAccountId)}";
            return _http.GetObject<ConnectedAccount>(path, ConnectedAccountEnvelope);
        }

        public ListResponse<ConnectedAccountSummary> ListConnectedAccounts(
            ConnectedAccountListParams? parameters = null)
        {
            var query = parameters?.ToQuery();
            return _http.ListObjects<ConnectedAccountSummary>(
                "/connectedaccounts", query, ConnectedAccountsEnvelope);
        }

        public ListResponse<ConnectedAccountPayoutSummary> ListConnectedAccountPayouts(string connectedAccountId)
        {
            RequireConnectedAccountId(connectedAccountId);
            var path = $"/connectedaccounts/{ZohoHttpClient.EncodePath(connectedAccountId)}/payouts";
            return _http.ListObjects<ConnectedAccountPayoutSummary>(path, null, PayoutsEnvelope);
        }

        public ConnectedAccountPayout GetConnectedAccountPayout(string connectedAccountId, string payoutId)
        {
            RequireConnectedAccountId(connectedAccountId);
            Require(payoutId, "payoutId", nameof(payoutId));
            var path = $"/connectedaccounts/{ZohoHttpClient.EncodePath(connectedAccountId)}"
                + $"/payouts/{ZohoHttpClient.EncodePath(payoutId)}";
            return _http.GetObject<ConnectedAccountPayout>(path, PayoutEnvelope);
        }

        public ListResponse<ConnectedAccountPayoutTransaction> ListConnectedAccountPayoutTransactions(
            string connectedAccountId,
            string payoutId)
        {
            RequireConnectedAccountId(connectedAccountId);
            Require(payoutId, "payoutId", nameof(payoutId));
            var path = $"/connectedaccounts/{ZohoHttpClient.EncodePath(connectedAccountId)}"
                + $"/payouts/{ZohoHttpClient.EncodePath(payoutId)}/transactions";
            return _http.ListObjects<ConnectedAccountPayoutTransaction>(path, null, TransactionsEnvelope);
        }

        public ListResponse<ConnectedAccountTransaction> ListConnectedAccountTransactions(
            string connectedAccountId,
            ConnectedAccountTransactionListParams? parameters = null)
        {
            RequireConnectedAccountId(connectedAccountId);
            var path = $"/connectedaccounts/{ZohoHttpClient.EncodePath(connectedAccountId)}/transactions";
            var query = parameters?.ToQuery();
            return _http.ListObjects<ConnectedAccountTransaction>(path, query, TransactionsEnvelope);
        }

        private static void RequireConnectedAccountId(string connectedAccountId) =>
            Require(connectedAccountId, "connectedAccountId", nameof(connectedAccountId));

        private static void Require(string value, string label, string paramName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"{label} is required", paramName);
            }
        }

        private static void RequireParams(object? parameters)
        {
            if (parameters is null)
            {
                throw new ArgumentException("params is required", nameof(parameters));
            }
        }
    }
}
