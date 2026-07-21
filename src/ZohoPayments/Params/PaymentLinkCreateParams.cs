using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZohoPayments.Params
{
    public sealed class PaymentLinkConfigurationsParams
    {
        public PaymentLinkConfigurationsParams(IReadOnlyList<string>? allowedPaymentMethods = null)
        {
            AllowedPaymentMethods = allowedPaymentMethods;
        }

        [JsonPropertyName("allowed_payment_methods")]
        public IReadOnlyList<string>? AllowedPaymentMethods { get; }
    }

    public sealed class PaymentLinkCreateParams
    {
        public PaymentLinkCreateParams(
            double amount,
            string currency,
            string description,
            string? email = null,
            string? phone = null,
            string? phoneCountryCode = null,
            string? expiresAt = null,
            string? referenceId = null,
            string? returnUrl = null,
            NotifyCustomerParams? notifyCustomer = null,
            PaymentLinkConfigurationsParams? configurations = null)
        {
            if (string.IsNullOrEmpty(currency))
            {
                throw new ArgumentException("currency is required", nameof(currency));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("description is required", nameof(description));
            }

            ParamValidator.ValidateDescription(description);
            ParamValidator.ValidateReferenceNumber(referenceId);

            Amount = amount;
            Currency = currency;
            Description = description;
            Email = email;
            Phone = phone;
            PhoneCountryCode = phoneCountryCode;
            ExpiresAt = expiresAt;
            ReferenceId = referenceId;
            ReturnUrl = returnUrl;
            NotifyCustomer = notifyCustomer;
            Configurations = configurations;
        }

        [JsonPropertyName("amount")]
        public double Amount { get; }

        [JsonPropertyName("currency")]
        public string Currency { get; }

        [JsonPropertyName("description")]
        public string Description { get; }

        [JsonPropertyName("email")]
        public string? Email { get; }

        [JsonPropertyName("phone")]
        public string? Phone { get; }

        [JsonPropertyName("phone_country_code")]
        public string? PhoneCountryCode { get; }

        [JsonPropertyName("expires_at")]
        public string? ExpiresAt { get; }

        [JsonPropertyName("reference_id")]
        public string? ReferenceId { get; }

        [JsonPropertyName("return_url")]
        public string? ReturnUrl { get; }

        [JsonPropertyName("notify_customer")]
        public NotifyCustomerParams? NotifyCustomer { get; }

        [JsonPropertyName("configurations")]
        public PaymentLinkConfigurationsParams? Configurations { get; }
    }

    /// <summary>All fields are optional, but at least one must be provided.</summary>
    public sealed class PaymentLinkUpdateParams
    {
        public PaymentLinkUpdateParams(
            string? description = null,
            string? email = null,
            string? phone = null,
            string? phoneCountryCode = null,
            string? expiresAt = null,
            string? referenceId = null,
            string? returnUrl = null,
            NotifyCustomerParams? notifyCustomer = null,
            PaymentLinkConfigurationsParams? configurations = null)
        {
            ParamValidator.RequireAnyField(description, email, phone, phoneCountryCode, expiresAt, referenceId, returnUrl, notifyCustomer, configurations);
            ParamValidator.ValidateDescription(description);
            ParamValidator.ValidateReferenceNumber(referenceId);

            Description = description;
            Email = email;
            Phone = phone;
            PhoneCountryCode = phoneCountryCode;
            ExpiresAt = expiresAt;
            ReferenceId = referenceId;
            ReturnUrl = returnUrl;
            NotifyCustomer = notifyCustomer;
            Configurations = configurations;
        }

        [JsonPropertyName("description")]
        public string? Description { get; }

        [JsonPropertyName("email")]
        public string? Email { get; }

        [JsonPropertyName("phone")]
        public string? Phone { get; }

        [JsonPropertyName("phone_country_code")]
        public string? PhoneCountryCode { get; }

        [JsonPropertyName("expires_at")]
        public string? ExpiresAt { get; }

        [JsonPropertyName("reference_id")]
        public string? ReferenceId { get; }

        [JsonPropertyName("return_url")]
        public string? ReturnUrl { get; }

        [JsonPropertyName("notify_customer")]
        public NotifyCustomerParams? NotifyCustomer { get; }

        [JsonPropertyName("configurations")]
        public PaymentLinkConfigurationsParams? Configurations { get; }
    }
}
