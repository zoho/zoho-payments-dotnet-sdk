using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZohoPayments.Models
{
    /// <summary>Payment the transfer was split out of (<c>Transfer.PaymentDetails</c>).</summary>
    public sealed class TransferPaymentDetails
    {
        [JsonPropertyName("amount")]
        public string? Amount { get; set; }

        [JsonPropertyName("amount_formatted")]
        public string? AmountFormatted { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("payment_date")]
        public long? PaymentDate { get; set; }

        [JsonPropertyName("payment_date_formatted")]
        public string? PaymentDateFormatted { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("status_formatted")]
        public string? StatusFormatted { get; set; }
    }

    /// <summary>Reversal entry listed inside a transfer (<c>Transfer.Reversal</c>).</summary>
    public sealed class TransferReversalEntry
    {
        [JsonPropertyName("reversal_id")]
        public string? ReversalId { get; set; }

        [JsonPropertyName("total_amount")]
        public string? TotalAmount { get; set; }

        [JsonPropertyName("net_amount")]
        public string? NetAmount { get; set; }

        [JsonPropertyName("fee_amount")]
        public string? FeeAmount { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }
    }

    public sealed class Transfer
    {
        [JsonPropertyName("transfer_id")]
        public string? TransferId { get; set; }

        [JsonPropertyName("payment_id")]
        public string? PaymentId { get; set; }

        [JsonPropertyName("total_amount")]
        public string? TotalAmount { get; set; }

        [JsonPropertyName("net_amount")]
        public string? NetAmount { get; set; }

        [JsonPropertyName("fee_amount")]
        public string? FeeAmount { get; set; }

        [JsonPropertyName("fee_rate")]
        public string? FeeRate { get; set; }

        [JsonPropertyName("fee_tax_amount")]
        public string? FeeTaxAmount { get; set; }

        [JsonPropertyName("fee_tax_rate")]
        public string? FeeTaxRate { get; set; }

        [JsonPropertyName("connected_account_id")]
        public string? ConnectedAccountId { get; set; }

        [JsonPropertyName("connected_account_name")]
        public string? ConnectedAccountName { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("failure_code")]
        public string? FailureCode { get; set; }

        [JsonPropertyName("reversed_amount")]
        public string? ReversedAmount { get; set; }

        [JsonPropertyName("available_amount_for_reversal")]
        public string? AvailableAmountForReversal { get; set; }

        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }

        [JsonPropertyName("reversals")]
        public List<TransferReversalEntry>? Reversals { get; set; }

        [JsonPropertyName("payment_details")]
        public TransferPaymentDetails? PaymentDetails { get; set; }
    }

    /// <summary>Transfer summary returned in list API responses.</summary>
    public sealed class TransferSummary
    {
        [JsonPropertyName("transfer_id")]
        public string? TransferId { get; set; }

        [JsonPropertyName("payment_id")]
        public string? PaymentId { get; set; }

        [JsonPropertyName("total_amount")]
        public string? TotalAmount { get; set; }

        [JsonPropertyName("net_amount")]
        public string? NetAmount { get; set; }

        [JsonPropertyName("fee_amount")]
        public string? FeeAmount { get; set; }

        [JsonPropertyName("fee_rate")]
        public string? FeeRate { get; set; }

        [JsonPropertyName("fee_tax_amount")]
        public string? FeeTaxAmount { get; set; }

        [JsonPropertyName("connected_account_id")]
        public string? ConnectedAccountId { get; set; }

        [JsonPropertyName("connected_account_name")]
        public string? ConnectedAccountName { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("failure_code")]
        public string? FailureCode { get; set; }

        [JsonPropertyName("reversed_amount")]
        public string? ReversedAmount { get; set; }

        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }
    }

    /// <summary>Per-connected-account outcome of a transfer create request (<c>TransferCreateResponse.Split</c>).</summary>
    public sealed class TransferSplit
    {
        [JsonPropertyName("transfer_id")]
        public string? TransferId { get; set; }

        [JsonPropertyName("connected_account_id")]
        public string? ConnectedAccountId { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("transfer_amount")]
        public string? TransferAmount { get; set; }

        [JsonPropertyName("net_transfer_amount")]
        public string? NetTransferAmount { get; set; }

        [JsonPropertyName("fee_amount")]
        public string? FeeAmount { get; set; }

        [JsonPropertyName("fee_tax_amount")]
        public string? FeeTaxAmount { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("error_code")]
        public string? ErrorCode { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }

    /// <summary>
    /// Response of a transfer create request. A request may partially succeed — inspect each
    /// entry in <see cref="Splits"/> for its own status and error code.
    /// </summary>
    public sealed class TransferCreateResponse
    {
        [JsonPropertyName("splits")]
        public List<TransferSplit>? Splits { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }
}
