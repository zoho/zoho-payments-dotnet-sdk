using System.Text.Json.Serialization;

namespace ZohoPayments.Models
{
    public sealed class CardChecks
    {
        [JsonPropertyName("address_line_check")]
        public string? AddressLineCheck { get; set; }

        [JsonPropertyName("postal_code_check")]
        public string? PostalCodeCheck { get; set; }

        [JsonPropertyName("cvc_check")]
        public string? CvcCheck { get; set; }
    }

    public sealed class SavedCardDetail
    {
        [JsonPropertyName("card_holder_name")]
        public string? CardHolderName { get; set; }

        [JsonPropertyName("last_four_digits")]
        public string? LastFourDigits { get; set; }

        [JsonPropertyName("expiry_month")]
        public string? ExpiryMonth { get; set; }

        [JsonPropertyName("expiry_year")]
        public string? ExpiryYear { get; set; }

        [JsonPropertyName("brand")]
        public string? Brand { get; set; }

        [JsonPropertyName("funding")]
        public string? Funding { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("card_checks")]
        public CardChecks? CardChecks { get; set; }
    }

    public sealed class CardDetail
    {
        [JsonPropertyName("card_holder_name")]
        public string? CardHolderName { get; set; }

        [JsonPropertyName("last_four_digits")]
        public string? LastFourDigits { get; set; }

        [JsonPropertyName("expiry_month")]
        public string? ExpiryMonth { get; set; }

        [JsonPropertyName("expiry_year")]
        public string? ExpiryYear { get; set; }

        [JsonPropertyName("brand")]
        public string? Brand { get; set; }

        [JsonPropertyName("funding")]
        public string? Funding { get; set; }
    }

    public sealed class AchDebitDetail
    {
        [JsonPropertyName("account_holder_name")]
        public string? AccountHolderName { get; set; }

        [JsonPropertyName("last_four_digits")]
        public string? LastFourDigits { get; set; }

        [JsonPropertyName("account_holder_type")]
        public string? AccountHolderType { get; set; }

        [JsonPropertyName("account_type")]
        public string? AccountType { get; set; }

        [JsonPropertyName("bank_name")]
        public string? BankName { get; set; }

        [JsonPropertyName("routing_number")]
        public string? RoutingNumber { get; set; }
    }

    public sealed class AddressDetail
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("address_id")]
        public string? AddressId { get; set; }

        [JsonPropertyName("address_line1")]
        public string? AddressLine1 { get; set; }

        [JsonPropertyName("address_line2")]
        public string? AddressLine2 { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("postal_code")]
        public string? PostalCode { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }
    }

    // IN-only nested payment-method details.

    public sealed class Upi
    {
        [JsonPropertyName("upi_id")]
        public string? UpiId { get; set; }

        [JsonPropertyName("channel")]
        public string? Channel { get; set; }

        [JsonPropertyName("account_type")]
        public string? AccountType { get; set; }
    }

    public sealed class NetBanking
    {
        [JsonPropertyName("bank_name")]
        public string? BankName { get; set; }
    }

    public sealed class BankTransfer
    {
        [JsonPropertyName("virtual_account_number")]
        public string? VirtualAccountNumber { get; set; }

        [JsonPropertyName("mode")]
        public string? Mode { get; set; }

        [JsonPropertyName("payer_name")]
        public string? PayerName { get; set; }

        [JsonPropertyName("payer_account_no")]
        public string? PayerAccountNo { get; set; }

        [JsonPropertyName("payer_ifsc_code")]
        public string? PayerIfscCode { get; set; }
    }

    /// <summary>Payment method snapshot attached to payments, mandates, etc.</summary>
    public sealed class PaymentMethodDetail
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("mandate_id")]
        public string? MandateId { get; set; }

        [JsonPropertyName("card")]
        public CardDetail? Card { get; set; }

        [JsonPropertyName("ach_debit")]
        public AchDebitDetail? AchDebit { get; set; }

        [JsonPropertyName("upi")]
        public Upi? Upi { get; set; }

        [JsonPropertyName("net_banking")]
        public NetBanking? NetBanking { get; set; }

        [JsonPropertyName("bank_transfer")]
        public BankTransfer? BankTransfer { get; set; }
    }

    // Saved payment method (/paymentmethods).

    public sealed class PaymentMethod
    {
        [JsonPropertyName("payment_method_id")]
        public string? PaymentMethodId { get; set; }

        [JsonPropertyName("customer_id")]
        public string? CustomerId { get; set; }

        [JsonPropertyName("customer_name")]
        public string? CustomerName { get; set; }

        [JsonPropertyName("customer_email")]
        public string? CustomerEmail { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("source")]
        public string? Source { get; set; }

        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }

        [JsonPropertyName("last_modified_time")]
        public long? LastModifiedTime { get; set; }

        [JsonPropertyName("card")]
        public SavedCardDetail? Card { get; set; }

        [JsonPropertyName("ach_debit")]
        public AchDebitDetail? AchDebit { get; set; }

        [JsonPropertyName("billing_address")]
        public AddressDetail? BillingAddress { get; set; }
    }

    public sealed class PaymentMethodSessionDetail
    {
        [JsonPropertyName("payment_method_id")]
        public string? PaymentMethodId { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    public sealed class PaymentMethodSession
    {
        [JsonPropertyName("payment_method_session_id")]
        public string? PaymentMethodSessionId { get; set; }

        [JsonPropertyName("customer_id")]
        public string? CustomerId { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("payment_method")]
        public PaymentMethodSessionDetail? PaymentMethod { get; set; }
    }
}
