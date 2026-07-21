using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZohoPayments.Models
{
    /// <summary>Minimal {payment_method_id, type} block used in list responses.</summary>
    public sealed class PaymentMethodSummary
    {
        [JsonPropertyName("payment_method_id")]
        public string? PaymentMethodId { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    public sealed class PaymentSummary
    {
        [JsonPropertyName("payment_id")]
        public string? PaymentId { get; set; }

        [JsonPropertyName("amount")]
        public string? Amount { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("receipt_email")]
        public string? ReceiptEmail { get; set; }

        [JsonPropertyName("reference_number")]
        public string? ReferenceNumber { get; set; }

        [JsonPropertyName("amount_captured")]
        public string? AmountCaptured { get; set; }

        [JsonPropertyName("amount_refunded")]
        public string? AmountRefunded { get; set; }

        [JsonPropertyName("fee_amount")]
        public string? FeeAmount { get; set; }

        [JsonPropertyName("net_amount")]
        public string? NetAmount { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("date")]
        public long? Date { get; set; }

        [JsonPropertyName("payment_method")]
        public PaymentMethodSummary? PaymentMethod { get; set; }
    }

    public sealed class Payment
    {
        [JsonPropertyName("payment_id")]
        public string? PaymentId { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("amount")]
        public string? Amount { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("payments_session_id")]
        public string? PaymentsSessionId { get; set; }

        [JsonPropertyName("receipt_email")]
        public string? ReceiptEmail { get; set; }

        [JsonPropertyName("reference_number")]
        public string? ReferenceNumber { get; set; }

        [JsonPropertyName("transaction_reference_number")]
        public string? TransactionReferenceNumber { get; set; }

        [JsonPropertyName("invoice_number")]
        public string? InvoiceNumber { get; set; }

        [JsonPropertyName("amount_captured")]
        public string? AmountCaptured { get; set; }

        [JsonPropertyName("amount_refunded")]
        public string? AmountRefunded { get; set; }

        [JsonPropertyName("fee_amount")]
        public string? FeeAmount { get; set; }

        [JsonPropertyName("net_tax_amount")]
        public string? NetTaxAmount { get; set; }

        [JsonPropertyName("total_fee_amount")]
        public string? TotalFeeAmount { get; set; }

        [JsonPropertyName("net_amount")]
        public string? NetAmount { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("exchange_rate")]
        public double? ExchangeRate { get; set; }

        [JsonPropertyName("statement_descriptor")]
        public string? StatementDescriptor { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("date")]
        public long? Date { get; set; }

        [JsonPropertyName("payment_method")]
        public PaymentMethodDetail? PaymentMethod { get; set; }

        [JsonPropertyName("meta_data")]
        public List<MetaData> MetaData { get; set; } = new List<MetaData>();
    }
}
