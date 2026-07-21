using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZohoPayments.Models
{
    public sealed class VirtualAccount
    {
        [JsonPropertyName("virtual_account_id")]
        public string? VirtualAccountId { get; set; }

        [JsonPropertyName("account_number")]
        public string? AccountNumber { get; set; }

        [JsonPropertyName("ifsc_code")]
        public string? IfscCode { get; set; }

        [JsonPropertyName("beneficiary_name")]
        public string? BeneficiaryName { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("customer_id")]
        public string? CustomerId { get; set; }

        [JsonPropertyName("reference_number")]
        public string? ReferenceNumber { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("expires_at")]
        public string? ExpiresAt { get; set; }

        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }

        [JsonPropertyName("last_modified_time")]
        public long? LastModifiedTime { get; set; }

        [JsonPropertyName("meta_data")]
        public List<MetaData> MetaData { get; set; } = new List<MetaData>();

        [JsonPropertyName("minimum_amount")]
        public double? MinimumAmount { get; set; }

        [JsonPropertyName("maximum_amount")]
        public double? MaximumAmount { get; set; }

        [JsonPropertyName("amount_paid")]
        public double? AmountPaid { get; set; }
    }

    public sealed class VirtualAccountPaymentMethod
    {
        [JsonPropertyName("payment_method_id")]
        public string? PaymentMethodId { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    public sealed class VirtualAccountPayment
    {
        [JsonPropertyName("payment_id")]
        public string? PaymentId { get; set; }

        [JsonPropertyName("customer_id")]
        public string? CustomerId { get; set; }

        [JsonPropertyName("virtual_account_id")]
        public string? VirtualAccountId { get; set; }

        [JsonPropertyName("customer_name")]
        public string? CustomerName { get; set; }

        [JsonPropertyName("customer_email")]
        public string? CustomerEmail { get; set; }

        [JsonPropertyName("amount")]
        public string? Amount { get; set; }

        [JsonPropertyName("receipt_email")]
        public string? ReceiptEmail { get; set; }

        [JsonPropertyName("dialing_code")]
        public string? DialingCode { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("reference_number")]
        public string? ReferenceNumber { get; set; }

        [JsonPropertyName("transaction_reference_number")]
        public string? TransactionReferenceNumber { get; set; }

        [JsonPropertyName("payment_type")]
        public string? PaymentType { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("balance")]
        public string? Balance { get; set; }

        [JsonPropertyName("amount_captured")]
        public string? AmountCaptured { get; set; }

        [JsonPropertyName("amount_refunded")]
        public string? AmountRefunded { get; set; }

        [JsonPropertyName("fee_amount")]
        public string? FeeAmount { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("transaction_type")]
        public string? TransactionType { get; set; }

        [JsonPropertyName("fraud_alert")]
        public string? FraudAlert { get; set; }

        [JsonPropertyName("failure_reason")]
        public string? FailureReason { get; set; }

        [JsonPropertyName("failure_category")]
        public string? FailureCategory { get; set; }

        [JsonPropertyName("next_action")]
        public string? NextAction { get; set; }

        [JsonPropertyName("tip")]
        public string? Tip { get; set; }

        [JsonPropertyName("date")]
        public long? Date { get; set; }

        [JsonPropertyName("payment_method")]
        public VirtualAccountPaymentMethod? PaymentMethod { get; set; }
    }
}
