using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZohoPayments.Models
{
    /// <summary>Customer summary returned in list API responses (US only).</summary>
    public sealed class CustomerSummary
    {
        [JsonPropertyName("customer_id")]
        public string? CustomerId { get; set; }

        [JsonPropertyName("customer_name")]
        public string? CustomerName { get; set; }

        [JsonPropertyName("customer_email")]
        public string? CustomerEmail { get; set; }

        [JsonPropertyName("customer_phone")]
        public string? CustomerPhone { get; set; }

        [JsonPropertyName("customer_status")]
        public string? CustomerStatus { get; set; }

        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }

        [JsonPropertyName("last_modified_time")]
        public long? LastModifiedTime { get; set; }
    }

    public sealed class CustomerCard
    {
        [JsonPropertyName("card_holder_name")]
        public string? CardHolderName { get; set; }

        [JsonPropertyName("last_four_digits")]
        public string? LastFourDigits { get; set; }

        [JsonPropertyName("expiry_month")]
        public string? ExpiryMonth { get; set; }

        [JsonPropertyName("expiry_year")]
        public string? ExpiryYear { get; set; }
    }

    public sealed class CustomerAchDebit
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

    public sealed class CustomerPaymentMethod
    {
        [JsonPropertyName("payment_method_id")]
        public string? PaymentMethodId { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("brand")]
        public string? Brand { get; set; }

        [JsonPropertyName("last_four_digits")]
        public string? LastFourDigits { get; set; }

        [JsonPropertyName("expiry_month")]
        public string? ExpiryMonth { get; set; }

        [JsonPropertyName("expiry_year")]
        public string? ExpiryYear { get; set; }

        [JsonPropertyName("card")]
        public CustomerCard? Card { get; set; }

        [JsonPropertyName("ach_debit")]
        public CustomerAchDebit? AchDebit { get; set; }
    }

    public sealed class Customer
    {
        [JsonPropertyName("customer_id")]
        public string? CustomerId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("dialing_code")]
        public string? DialingCode { get; set; }

        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }

        [JsonPropertyName("last_modified_time")]
        public long? LastModifiedTime { get; set; }

        [JsonPropertyName("meta_data")]
        public List<MetaData> MetaData { get; set; } = new List<MetaData>();

        [JsonPropertyName("payment_methods")]
        public List<CustomerPaymentMethod> PaymentMethods { get; set; } = new List<CustomerPaymentMethod>();
    }
}
