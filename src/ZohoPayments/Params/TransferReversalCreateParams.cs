using System;
using System.Text.Json.Serialization;
using ZohoPayments.Internal;

namespace ZohoPayments.Params
{
    /// <summary>Body for <c>POST /transferreversals</c>.</summary>
    public sealed class TransferReversalCreateParams
    {
        public TransferReversalCreateParams(
            string transferId,
            string reversalAmount,
            string? description = null)
        {
            if (string.IsNullOrEmpty(transferId))
            {
                throw new ArgumentException("transferId is required", nameof(transferId));
            }

            if (string.IsNullOrEmpty(reversalAmount))
            {
                throw new ArgumentException("reversalAmount is required", nameof(reversalAmount));
            }

            ParamValidator.ValidateDescription(description);

            TransferId = transferId;
            ReversalAmount = reversalAmount;
            Description = description;
        }

        [JsonPropertyName("transfer_id")]
        public string TransferId { get; }

        [JsonPropertyName("reversal_amount")]
        public string ReversalAmount { get; }

        [JsonPropertyName("description")]
        public string? Description { get; }
    }

    /// <summary>Query parameters for <c>GET /transferreversals</c>.</summary>
    public sealed class TransferReversalListParams : IPaginationParams
    {
        public TransferReversalListParams(
            string? status = null,
            string? filterBy = null,
            string? paymentId = null,
            string? connectedAccountId = null,
            string? transferId = null,
            string? refundId = null,
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
            TransferId = transferId;
            RefundId = refundId;
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

        public string? TransferId { get; }

        public string? RefundId { get; }

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
                .Add("transfer_id", TransferId)
                .Add("refund_id", RefundId)
                .Add("from_date", FromDate)
                .Add("to_date", ToDate)
                .Add("sort_column", SortColumn)
                .Add("sort_order", SortOrder)
                .Add("search_text", SearchText)
                .Add("per_page", PerPage)
                .Add("page", Page);
    }
}
