using System;
using System.Text.Json.Serialization;

namespace ZohoPayments.Params
{
    public sealed class CardUpdate
    {
        public CardUpdate(string? expiryMonth = null, string? expiryYear = null)
        {
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
        }

        [JsonPropertyName("expiry_month")]
        public string? ExpiryMonth { get; }

        [JsonPropertyName("expiry_year")]
        public string? ExpiryYear { get; }
    }

    public sealed class AchDebitUpdate
    {
        public AchDebitUpdate(string? accountHolderType = null)
        {
            AccountHolderType = accountHolderType;
        }

        [JsonPropertyName("account_holder_type")]
        public string? AccountHolderType { get; }
    }

    /// <summary>Payment method update params (US edition).</summary>
    public sealed class PaymentMethodUpdateParams
    {
        public PaymentMethodUpdateParams(
            string type,
            CardUpdate? card = null,
            AchDebitUpdate? achDebit = null,
            PostalAddressParams? billingAddress = null)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("type is required", nameof(type));
            }

            Type = type;
            Card = card;
            AchDebit = achDebit;
            BillingAddress = billingAddress;
        }

        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("card")]
        public CardUpdate? Card { get; }

        [JsonPropertyName("ach_debit")]
        public AchDebitUpdate? AchDebit { get; }

        [JsonPropertyName("billing_address")]
        public PostalAddressParams? BillingAddress { get; }
    }

    /// <summary>Payment method session creation params (US edition).</summary>
    public sealed class PaymentMethodSessionCreateParams
    {
        public PaymentMethodSessionCreateParams(string customerId, string? description = null)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                throw new ArgumentException("customer_id is required", nameof(customerId));
            }

            ParamValidator.ValidateDescription(description);

            CustomerId = customerId;
            Description = description;
        }

        [JsonPropertyName("customer_id")]
        public string CustomerId { get; }

        [JsonPropertyName("description")]
        public string? Description { get; }
    }
}
