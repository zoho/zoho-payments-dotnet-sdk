namespace ZohoPayments.Params
{
    public interface IPaginationParams
    {
        int? PerPage { get; }

        int? Page { get; }
    }
}
