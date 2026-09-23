using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZohoPayments.Models
{
    /// <summary>Settlement bank account linked to a connected account.</summary>
    public sealed class ConnectedAccountBankAccount
    {
        [JsonPropertyName("connected_account_bank_account_id")]
        public string? ConnectedAccountBankAccountId { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("routing_number")]
        public string? RoutingNumber { get; set; }

        [JsonPropertyName("last_four_digits")]
        public string? LastFourDigits { get; set; }

        [JsonPropertyName("created_by")]
        public string? CreatedBy { get; set; }

        [JsonPropertyName("last_modified_by")]
        public string? LastModifiedBy { get; set; }

        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }

        [JsonPropertyName("last_modified_time")]
        public long? LastModifiedTime { get; set; }
    }

    public sealed class ConnectedAccount
    {
        [JsonPropertyName("connected_account_id")]
        public string? ConnectedAccountId { get; set; }

        [JsonPropertyName("email_id")]
        public string? EmailId { get; set; }

        [JsonPropertyName("account_name")]
        public string? AccountName { get; set; }

        [JsonPropertyName("pan")]
        public string? Pan { get; set; }

        [JsonPropertyName("mcc")]
        public string? Mcc { get; set; }

        [JsonPropertyName("business_description")]
        public string? BusinessDescription { get; set; }

        [JsonPropertyName("under_writing_status")]
        public string? UnderWritingStatus { get; set; }

        [JsonPropertyName("transfer_status")]
        public string? TransferStatus { get; set; }

        [JsonPropertyName("payout_status")]
        public string? PayoutStatus { get; set; }

        [JsonPropertyName("payout_delay_days")]
        public int? PayoutDelayDays { get; set; }

        [JsonPropertyName("payout_statement_descriptor")]
        public string? PayoutStatementDescriptor { get; set; }

        [JsonPropertyName("statement_descriptor_restricted_chars")]
        public string? StatementDescriptorRestrictedChars { get; set; }

        [JsonPropertyName("created_by")]
        public string? CreatedBy { get; set; }

        [JsonPropertyName("last_modified_by")]
        public string? LastModifiedBy { get; set; }

        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }

        [JsonPropertyName("last_modified_time")]
        public long? LastModifiedTime { get; set; }

        [JsonPropertyName("connected_account_bank_accounts")]
        public List<ConnectedAccountBankAccount>? ConnectedAccountBankAccounts { get; set; }
    }

    /// <summary>Connected account summary returned in list API responses.</summary>
    public sealed class ConnectedAccountSummary
    {
        [JsonPropertyName("connected_account_id")]
        public string? ConnectedAccountId { get; set; }

        [JsonPropertyName("account_name")]
        public string? AccountName { get; set; }

        [JsonPropertyName("email_id")]
        public string? EmailId { get; set; }

        [JsonPropertyName("under_writing_status")]
        public string? UnderWritingStatus { get; set; }

        [JsonPropertyName("transfer_status")]
        public string? TransferStatus { get; set; }

        [JsonPropertyName("created_by")]
        public string? CreatedBy { get; set; }

        [JsonPropertyName("last_modified_by")]
        public string? LastModifiedBy { get; set; }

        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }

        [JsonPropertyName("last_modified_time")]
        public long? LastModifiedTime { get; set; }
    }

    /// <summary>Bank account destination in connected account payout lists (<c>ConnectedAccountPayoutSummary.BankAccountDetails</c>).</summary>
    public sealed class ConnectedAccountPayoutBankAccountDetails
    {
        [JsonPropertyName("bank_name")]
        public string? BankName { get; set; }

        [JsonPropertyName("account_number_last_four_digits")]
        public string? AccountNumberLastFourDigits { get; set; }
    }

    /// <summary>Connected account payout summary returned in list API responses.</summary>
    public sealed class ConnectedAccountPayoutSummary
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
        public ConnectedAccountPayoutBankAccountDetails? BankAccountDetails { get; set; }
    }

    /// <summary>Audit comment recorded against a connected account payout (<c>ConnectedAccountPayout.Comment</c>).</summary>
    public sealed class ConnectedAccountPayoutComment
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

    /// <summary>Per-transaction-type totals for a connected account payout (<c>ConnectedAccountPayout.TransactionBreakdown</c>).</summary>
    public sealed class ConnectedAccountPayoutTransactionBreakdown
    {
        [JsonPropertyName("transaction_type")]
        public string? TransactionType { get; set; }

        [JsonPropertyName("count")]
        public int? Count { get; set; }

        [JsonPropertyName("net_amount")]
        public string? NetAmount { get; set; }

        [JsonPropertyName("fee")]
        public string? Fee { get; set; }

        [JsonPropertyName("amount")]
        public string? Amount { get; set; }

        [JsonPropertyName("tax")]
        public string? Tax { get; set; }
    }

    /// <summary>Charge / refund / adjustment breakdown of a connected account payout (<c>ConnectedAccountPayout.TransactionSummary</c>).</summary>
    public sealed class ConnectedAccountPayoutTransactionSummary
    {
        [JsonPropertyName("charge")]
        public ConnectedAccountPayoutTransactionBreakdown? Charge { get; set; }

        [JsonPropertyName("refund")]
        public ConnectedAccountPayoutTransactionBreakdown? Refund { get; set; }

        [JsonPropertyName("adjustment")]
        public ConnectedAccountPayoutTransactionBreakdown? Adjustment { get; set; }

        [JsonPropertyName("total_amount")]
        public string? TotalAmount { get; set; }
    }

    /// <summary>Settlement bank account a connected account payout was credited to (<c>ConnectedAccountPayout.AccountDetails</c>).</summary>
    public sealed class ConnectedAccountPayoutAccountDetails
    {
        [JsonPropertyName("bank_name")]
        public string? BankName { get; set; }

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

    public sealed class ConnectedAccountPayout
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

        [JsonPropertyName("comments")]
        public List<ConnectedAccountPayoutComment>? Comments { get; set; }

        [JsonPropertyName("transaction_summary")]
        public ConnectedAccountPayoutTransactionSummary? TransactionSummary { get; set; }

        [JsonPropertyName("account_details")]
        public ConnectedAccountPayoutAccountDetails? AccountDetails { get; set; }
    }

    /// <summary>A single transaction that makes up a connected account payout.</summary>
    public sealed class ConnectedAccountPayoutTransaction
    {
        [JsonPropertyName("payout_id")]
        public string? PayoutId { get; set; }

        [JsonPropertyName("merchant_transaction_id")]
        public string? MerchantTransactionId { get; set; }

        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }

        [JsonPropertyName("parent_transaction_id")]
        public string? ParentTransactionId { get; set; }

        [JsonPropertyName("transaction_type")]
        public string? TransactionType { get; set; }

        [JsonPropertyName("transaction_time")]
        public long? TransactionTime { get; set; }

        [JsonPropertyName("net_amount")]
        public string? NetAmount { get; set; }

        [JsonPropertyName("amount")]
        public string? Amount { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("customer_id")]
        public string? CustomerId { get; set; }

        [JsonPropertyName("customer_name")]
        public string? CustomerName { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    /// <summary>Balance transaction on a connected account's ledger.</summary>
    public sealed class ConnectedAccountTransaction
    {
        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }

        [JsonPropertyName("parent_transaction_id")]
        public string? ParentTransactionId { get; set; }

        [JsonPropertyName("transaction_type")]
        public string? TransactionType { get; set; }

        [JsonPropertyName("transaction_time")]
        public long? TransactionTime { get; set; }

        [JsonPropertyName("net_amount")]
        public string? NetAmount { get; set; }

        [JsonPropertyName("amount")]
        public string? Amount { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("available_on")]
        public long? AvailableOn { get; set; }

        [JsonPropertyName("settled_time")]
        public long? SettledTime { get; set; }

        [JsonPropertyName("payment_method")]
        public string? PaymentMethod { get; set; }

        [JsonPropertyName("card_brand")]
        public string? CardBrand { get; set; }

        [JsonPropertyName("card_type")]
        public string? CardType { get; set; }

        [JsonPropertyName("customer_id")]
        public string? CustomerId { get; set; }

        [JsonPropertyName("customer_name")]
        public string? CustomerName { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("refund_reason")]
        public string? RefundReason { get; set; }
    }
}
