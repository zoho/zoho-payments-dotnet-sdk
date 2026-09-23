using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZohoPayments.Models
{
    /// <summary>Bank account destination reported in payout list responses (<c>Payout.BankAccountDetails</c>).</summary>
    public sealed class PayoutBankAccountDetails
    {
        [JsonPropertyName("bank_name")]
        public string? BankName { get; set; }

        [JsonPropertyName("account_number_last_four_digits")]
        public string? AccountNumberLastFourDigits { get; set; }

        [JsonPropertyName("account_holder_name")]
        public string? AccountHolderName { get; set; }

        [JsonPropertyName("routing_number")]
        public string? RoutingNumber { get; set; }
    }

    /// <summary>Payout summary returned in list API responses.</summary>
    public sealed class Payout
    {
        [JsonPropertyName("payout_id")]
        public string? PayoutId { get; set; }

        [JsonPropertyName("amount")]
        public string? Amount { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("failure_code")]
        public string? FailureCode { get; set; }

        [JsonPropertyName("failure_message")]
        public string? FailureMessage { get; set; }

        [JsonPropertyName("statement_descriptor")]
        public string? StatementDescriptor { get; set; }

        [JsonPropertyName("payout_method")]
        public string? PayoutMethod { get; set; }

        [JsonPropertyName("initiated_time")]
        public long? InitiatedTime { get; set; }

        [JsonPropertyName("arrival_date")]
        public string? ArrivalDate { get; set; }

        [JsonPropertyName("processed_date")]
        public string? ProcessedDate { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("payout_bank_reference_id")]
        public string? PayoutBankReferenceId { get; set; }

        [JsonPropertyName("bank_account_details")]
        public PayoutBankAccountDetails? BankAccountDetails { get; set; }
    }

    /// <summary>Per-transaction-type totals within a payout's transaction summary (<c>PayoutDetail.TypeSummary</c>).</summary>
    public sealed class PayoutTypeSummary
    {
        [JsonPropertyName("transaction_type")]
        public string? TransactionType { get; set; }

        [JsonPropertyName("transaction_type_formatted")]
        public string? TransactionTypeFormatted { get; set; }

        [JsonPropertyName("count")]
        public int? Count { get; set; }

        [JsonPropertyName("net_amount")]
        public string? NetAmount { get; set; }

        [JsonPropertyName("net_amount_formatted")]
        public string? NetAmountFormatted { get; set; }

        [JsonPropertyName("fee")]
        public string? Fee { get; set; }

        [JsonPropertyName("fee_formatted")]
        public string? FeeFormatted { get; set; }

        [JsonPropertyName("amount")]
        public string? Amount { get; set; }

        [JsonPropertyName("amount_formatted")]
        public string? AmountFormatted { get; set; }

        [JsonPropertyName("tax")]
        public string? Tax { get; set; }

        [JsonPropertyName("tax_formatted")]
        public string? TaxFormatted { get; set; }
    }

    /// <summary>Charge / refund / adjustment breakdown of the transactions inside a payout (<c>PayoutDetail.TransactionSummary</c>).</summary>
    public sealed class PayoutTransactionSummary
    {
        [JsonPropertyName("charge")]
        public PayoutTypeSummary? Charge { get; set; }

        [JsonPropertyName("refund")]
        public PayoutTypeSummary? Refund { get; set; }

        [JsonPropertyName("adjustment")]
        public PayoutTypeSummary? Adjustment { get; set; }

        [JsonPropertyName("total_amount")]
        public string? TotalAmount { get; set; }
    }

    /// <summary>Settlement bank account a payout was credited to (<c>PayoutDetail.AccountDetails</c>).</summary>
    public sealed class PayoutAccountDetails
    {
        [JsonPropertyName("bank_name")]
        public string? BankName { get; set; }

        [JsonPropertyName("account_holder")]
        public string? AccountHolder { get; set; }

        [JsonPropertyName("account_number_last_four_digits")]
        public string? AccountNumberLastFourDigits { get; set; }

        [JsonPropertyName("routing_number")]
        public string? RoutingNumber { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }
    }

    /// <summary>Audit comment recorded against a payout (<c>PayoutDetail.Comment</c>).</summary>
    public sealed class PayoutComment
    {
        [JsonPropertyName("comment_id")]
        public string? CommentId { get; set; }

        [JsonPropertyName("amount")]
        public string? Amount { get; set; }

        [JsonPropertyName("operation_type")]
        public string? OperationType { get; set; }

        [JsonPropertyName("action_type")]
        public string? ActionType { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("created_by")]
        public string? CreatedBy { get; set; }

        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }
    }

    public sealed class PayoutDetail
    {
        [JsonPropertyName("payout_id")]
        public string? PayoutId { get; set; }

        [JsonPropertyName("amount")]
        public string? Amount { get; set; }

        [JsonPropertyName("processing_fee")]
        public string? ProcessingFee { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("failure_code")]
        public string? FailureCode { get; set; }

        [JsonPropertyName("failure_message")]
        public string? FailureMessage { get; set; }

        [JsonPropertyName("statement_descriptor")]
        public string? StatementDescriptor { get; set; }

        [JsonPropertyName("payout_method")]
        public string? PayoutMethod { get; set; }

        [JsonPropertyName("initiated_time")]
        public long? InitiatedTime { get; set; }

        [JsonPropertyName("arrival_date")]
        public string? ArrivalDate { get; set; }

        [JsonPropertyName("processed_date")]
        public string? ProcessedDate { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("fee")]
        public string? Fee { get; set; }

        [JsonPropertyName("fee_rate")]
        public string? FeeRate { get; set; }

        [JsonPropertyName("fixed_fee")]
        public string? FixedFee { get; set; }

        [JsonPropertyName("tax_amount")]
        public string? TaxAmount { get; set; }

        [JsonPropertyName("tax_rate")]
        public string? TaxRate { get; set; }

        [JsonPropertyName("payout_bank_reference_id")]
        public string? PayoutBankReferenceId { get; set; }

        [JsonPropertyName("comments")]
        public List<PayoutComment>? Comments { get; set; }

        [JsonPropertyName("transaction_summary")]
        public PayoutTransactionSummary? TransactionSummary { get; set; }

        [JsonPropertyName("account_details")]
        public PayoutAccountDetails? AccountDetails { get; set; }
    }

    /// <summary>A single transaction that makes up a payout.</summary>
    public sealed class PayoutTransaction
    {
        [JsonPropertyName("payout_id")]
        public string? PayoutId { get; set; }

        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }

        [JsonPropertyName("parent_transaction_id")]
        public string? ParentTransactionId { get; set; }

        [JsonPropertyName("net_amount")]
        public string? NetAmount { get; set; }

        [JsonPropertyName("amount")]
        public string? Amount { get; set; }

        [JsonPropertyName("fee")]
        public string? Fee { get; set; }

        [JsonPropertyName("tax")]
        public string? Tax { get; set; }

        [JsonPropertyName("transaction_type")]
        public string? TransactionType { get; set; }

        [JsonPropertyName("transaction_time")]
        public long? TransactionTime { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("customer_id")]
        public string? CustomerId { get; set; }

        [JsonPropertyName("customer_name")]
        public string? CustomerName { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
