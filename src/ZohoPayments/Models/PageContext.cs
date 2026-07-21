using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZohoPayments.Models
{
    /// <summary>Pagination metadata returned alongside list responses.</summary>
    public sealed class PageContext
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("has_more_page")]
        public bool HasMorePage { get; set; }

        internal static PageContext FromJson(JsonElement data)
        {
            var pageContext = new PageContext();
            if (data.TryGetProperty("page", out var page) && page.ValueKind == JsonValueKind.Number)
            {
                pageContext.Page = page.GetInt32();
            }

            if (data.TryGetProperty("per_page", out var perPage) && perPage.ValueKind == JsonValueKind.Number)
            {
                pageContext.PerPage = perPage.GetInt32();
            }

            if (data.TryGetProperty("total", out var total) && total.ValueKind == JsonValueKind.Number)
            {
                pageContext.Total = total.GetInt32();
            }

            if (data.TryGetProperty("total_pages", out var totalPages) && totalPages.ValueKind == JsonValueKind.Number)
            {
                pageContext.TotalPages = totalPages.GetInt32();
            }

            if (data.TryGetProperty("has_more_page", out var hasMorePage) &&
                (hasMorePage.ValueKind == JsonValueKind.True || hasMorePage.ValueKind == JsonValueKind.False))
            {
                pageContext.HasMorePage = hasMorePage.GetBoolean();
            }

            return pageContext;
        }
    }
}
