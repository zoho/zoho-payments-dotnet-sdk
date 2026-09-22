using ZohoPayments.Internal;

namespace ZohoPayments.Params
{
    /// <summary>Query parameters for <c>GET /payouts</c>.</summary>
    public sealed class PayoutListParams : IPaginationParams
    {
        public PayoutListParams(
            string? status = null,
            string? filterBy = null,
            string? fromDate = null,
            string? toDate = null,
            int? perPage = null,
            int? page = null)
        {
            Status = status;
            FilterBy = filterBy;
            FromDate = fromDate;
            ToDate = toDate;
            PerPage = perPage;
            Page = page;
        }

        public string? Status { get; }

        public string? FilterBy { get; }

        public string? FromDate { get; }

        public string? ToDate { get; }

        public int? PerPage { get; }

        public int? Page { get; }

        internal QueryParams ToQuery() =>
            new QueryParams()
                .Add("status", Status)
                .Add("filter_by", FilterBy)
                .Add("from_date", FromDate)
                .Add("to_date", ToDate)
                .Add("per_page", PerPage)
                .Add("page", Page);
    }

    /// <summary>Query parameters for <c>GET /payouts/{payout_id}/transactions</c>.</summary>
    public sealed class PayoutTransactionListParams : IPaginationParams
    {
        public PayoutTransactionListParams(int? perPage = null, int? page = null)
        {
            PerPage = perPage;
            Page = page;
        }

        public int? PerPage { get; }

        public int? Page { get; }

        internal QueryParams ToQuery() =>
            new QueryParams()
                .Add("per_page", PerPage)
                .Add("page", Page);
    }
}
