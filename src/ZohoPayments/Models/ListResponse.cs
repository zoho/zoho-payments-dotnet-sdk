using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ZohoPayments.Models
{
    /// <summary>Immutable paginated list response: items + page context.</summary>
    public sealed class ListResponse<T>
    {
        public ListResponse(IList<T> data, PageContext pageContext)
        {
            Data = new ReadOnlyCollection<T>(data);
            PageContext = pageContext;
        }

        public IReadOnlyList<T> Data { get; }

        public PageContext PageContext { get; }
    }
}
