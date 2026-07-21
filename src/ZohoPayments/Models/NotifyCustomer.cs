using System.Text.Json.Serialization;

namespace ZohoPayments.Models
{
    public sealed class NotifyCustomer
    {
        [JsonPropertyName("email")]
        public bool? Email { get; set; }

        [JsonPropertyName("sms")]
        public bool? Sms { get; set; }
    }
}
