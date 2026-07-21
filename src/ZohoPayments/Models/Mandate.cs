using System.Text.Json.Serialization;

namespace ZohoPayments.Models
{
    public sealed class MandateUpi
    {
        [JsonPropertyName("upi_id")]
        public string? UpiId { get; set; }
    }

    /// <summary>Payment method on a mandate (UPI only).</summary>
    public sealed class MandatePaymentMethod
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("upi")]
        public MandateUpi? Upi { get; set; }
    }

    public sealed class Mandate
    {
        [JsonPropertyName("mandate_id")]
        public string? MandateId { get; set; }

        [JsonPropertyName("customer_id")]
        public string? CustomerId { get; set; }

        [JsonPropertyName("customer_name")]
        public string? CustomerName { get; set; }

        [JsonPropertyName("customer_email")]
        public string? CustomerEmail { get; set; }

        [JsonPropertyName("customer_phone")]
        public string? CustomerPhone { get; set; }

        [JsonPropertyName("amount")]
        public string? Amount { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("amount_rule")]
        public string? AmountRule { get; set; }

        [JsonPropertyName("frequency")]
        public string? Frequency { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("debit_day")]
        public int? DebitDay { get; set; }

        [JsonPropertyName("debit_rule")]
        public string? DebitRule { get; set; }

        [JsonPropertyName("start_date")]
        public long? StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public long? EndDate { get; set; }

        [JsonPropertyName("payment_method")]
        public MandatePaymentMethod? PaymentMethod { get; set; }
    }

    public sealed class MandateNotification
    {
        [JsonPropertyName("mandate_id")]
        public string? MandateId { get; set; }

        [JsonPropertyName("mandate_notification_id")]
        public string? MandateNotificationId { get; set; }

        [JsonPropertyName("customer_id")]
        public string? CustomerId { get; set; }

        [JsonPropertyName("mandate_amount")]
        public string? MandateAmount { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("amount_rule")]
        public string? AmountRule { get; set; }

        [JsonPropertyName("notification_amount")]
        public string? NotificationAmount { get; set; }

        [JsonPropertyName("notification_status")]
        public string? NotificationStatus { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("invoice_number")]
        public string? InvoiceNumber { get; set; }

        [JsonPropertyName("amount")]
        public string? Amount { get; set; }

        [JsonPropertyName("notification_date")]
        public long? NotificationDate { get; set; }

        [JsonPropertyName("execution_date")]
        public long? ExecutionDate { get; set; }

        [JsonPropertyName("payment_method")]
        public MandatePaymentMethod? PaymentMethod { get; set; }
    }

    /// <summary>Minimal {type} block on <see cref="MandatePayment.PaymentMethod"/>.</summary>
    public sealed class MandatePaymentPaymentMethod
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    public sealed class MandatePayment
    {
        [JsonPropertyName("payments_session_id")]
        public string? PaymentsSessionId { get; set; }

        [JsonPropertyName("invoice_number")]
        public string? InvoiceNumber { get; set; }

        [JsonPropertyName("customer_id")]
        public string? CustomerId { get; set; }

        [JsonPropertyName("amount")]
        public string? Amount { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("statement_descriptor")]
        public string? StatementDescriptor { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("reference_number")]
        public string? ReferenceNumber { get; set; }

        [JsonPropertyName("date")]
        public long? Date { get; set; }

        [JsonPropertyName("payment_method")]
        public MandatePaymentPaymentMethod? PaymentMethod { get; set; }
    }
}
