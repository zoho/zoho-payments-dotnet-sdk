using System.Text.Json.Serialization;

namespace ZohoPayments.Models
{
    public sealed class MetaData
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }
    }
}
