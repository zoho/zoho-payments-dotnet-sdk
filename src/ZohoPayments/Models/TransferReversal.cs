using System.Text.Json.Serialization;

namespace ZohoPayments.Models
{
    public sealed class TransferReversalDetail
    {
        [JsonPropertyName("transfer_reversal_id")]
        public string? TransferReversalId { get; set; }

        [JsonPropertyName("transfer_id")]
        public string? TransferId { get; set; }

        [JsonPropertyName("payment_id")]
        public string? PaymentId { get; set; }

        [JsonPropertyName("refund_id")]
        public string? RefundId { get; set; }

        [JsonPropertyName("total_amount")]
        public string? TotalAmount { get; set; }

        [JsonPropertyName("connected_account_id")]
        public string? ConnectedAccountId { get; set; }

        [JsonPropertyName("connected_account_name")]
        public string? ConnectedAccountName { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("failure_code")]
        public string? FailureCode { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }
    }

    /// <summary>Transfer reversal summary returned in list API responses.</summary>
    public sealed class TransferReversal
    {
        [JsonPropertyName("transfer_reversal_id")]
        public string? TransferReversalId { get; set; }

        [JsonPropertyName("transfer_id")]
        public string? TransferId { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("total_amount")]
        public string? TotalAmount { get; set; }

        [JsonPropertyName("connected_account_id")]
        public string? ConnectedAccountId { get; set; }

        [JsonPropertyName("connected_account_name")]
        public string? ConnectedAccountName { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("failure_code")]
        public string? FailureCode { get; set; }

        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }
    }

    /// <summary>Response of a transfer reversal create request.</summary>
    public sealed class TransferReversalCreateResponse
    {
        [JsonPropertyName("transfer_reversal_id")]
        public string? TransferReversalId { get; set; }

        [JsonPropertyName("transfer_id")]
        public string? TransferId { get; set; }

        [JsonPropertyName("transfer_reversal_amount")]
        public string? TransferReversalAmount { get; set; }

        [JsonPropertyName("connected_account_id")]
        public string? ConnectedAccountId { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }
}
