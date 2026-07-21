using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZohoPayments.Models
{
    public sealed class PaymentSessionPayment
    {
        [JsonPropertyName("payment_id")]
        public string? PaymentId { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }
    }

    public sealed class PaymentSession
    {
        [JsonPropertyName("payments_session_id")]
        public string? PaymentsSessionId { get; set; }

        [JsonPropertyName("access_key")]
        public string? AccessKey { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("amount")]
        public string? Amount { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }

        [JsonPropertyName("expiry_time")]
        public long? ExpiryTime { get; set; }

        [JsonPropertyName("payments")]
        public List<PaymentSessionPayment> Payments { get; set; } = new List<PaymentSessionPayment>();

        [JsonPropertyName("meta_data")]
        public List<MetaData> MetaData { get; set; } = new List<MetaData>();

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("invoice_number")]
        public string? InvoiceNumber { get; set; }

        [JsonPropertyName("reference_number")]
        public string? ReferenceNumber { get; set; }

        [JsonPropertyName("max_retry_count")]
        public int? MaxRetryCount { get; set; }

        [JsonPropertyName("configurations")]
        public Configurations? Configurations { get; set; }
    }
}
