using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using ZohoPayments.Internal;

namespace ZohoPayments.Params
{
    /// <summary>A single split entry within a transfer request.</summary>
    public sealed class TransferSplitParams
    {
        public TransferSplitParams(
            string connectedAccountId,
            string amount,
            string? description = null)
        {
            if (string.IsNullOrEmpty(connectedAccountId))
            {
                throw new ArgumentException("connectedAccountId is required", nameof(connectedAccountId));
            }

            if (string.IsNullOrEmpty(amount))
            {
                throw new ArgumentException("amount is required", nameof(amount));
            }

            ParamValidator.ValidateDescription(description);

            ConnectedAccountId = connectedAccountId;
            Amount = amount;
            Description = description;
        }

        [JsonPropertyName("connected_account_id")]
        public string ConnectedAccountId { get; }

        [JsonPropertyName("amount")]
        public string Amount { get; }

        [JsonPropertyName("description")]
        public string? Description { get; }
    }

    /// <summary>Body for <c>POST /transfers</c>.</summary>
    public sealed class TransferCreateParams
    {
        public TransferCreateParams(string paymentId, IReadOnlyList<TransferSplitParams> transferSplit)
        {
            if (string.IsNullOrEmpty(paymentId))
            {
                throw new ArgumentException("paymentId is required", nameof(paymentId));
            }

            if (transferSplit is null || transferSplit.Count == 0)
            {
                throw new ArgumentException("transferSplit is required", nameof(transferSplit));
            }

            PaymentId = paymentId;
            TransferSplit = new ReadOnlyCollection<TransferSplitParams>(
                new List<TransferSplitParams>(transferSplit));
        }

        [JsonPropertyName("payment_id")]
        public string PaymentId { get; }

        [JsonPropertyName("transfer_split")]
        public IReadOnlyList<TransferSplitParams> TransferSplit { get; }
    }

    /// <summary>Query parameters for <c>GET /transfers</c>.</summary>
    public sealed class TransferListParams : IPaginationParams
    {
        public TransferListParams(
            string? status = null,
            string? filterBy = null,
            string? paymentId = null,
            string? connectedAccountId = null,
            string? fromDate = null,
            string? toDate = null,
            string? sortColumn = null,
            string? sortOrder = null,
            string? searchText = null,
            int? perPage = null,
            int? page = null)
        {
            Status = status;
            FilterBy = filterBy;
            PaymentId = paymentId;
            ConnectedAccountId = connectedAccountId;
            FromDate = fromDate;
            ToDate = toDate;
            SortColumn = sortColumn;
            SortOrder = sortOrder;
            SearchText = searchText;
            PerPage = perPage;
            Page = page;
        }

        public string? Status { get; }

        public string? FilterBy { get; }

        public string? PaymentId { get; }

        public string? ConnectedAccountId { get; }

        public string? FromDate { get; }

        public string? ToDate { get; }

        public string? SortColumn { get; }

        public string? SortOrder { get; }

        public string? SearchText { get; }

        public int? PerPage { get; }

        public int? Page { get; }

        internal QueryParams ToQuery() =>
            new QueryParams()
                .Add("status", Status)
                .Add("filter_by", FilterBy)
                .Add("payment_id", PaymentId)
                .Add("connected_account_id", ConnectedAccountId)
                .Add("from_date", FromDate)
                .Add("to_date", ToDate)
                .Add("sort_column", SortColumn)
                .Add("sort_order", SortOrder)
                .Add("search_text", SearchText)
                .Add("per_page", PerPage)
                .Add("page", Page);
    }
}
