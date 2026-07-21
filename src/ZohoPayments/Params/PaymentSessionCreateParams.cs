using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZohoPayments.Params
{
    public sealed class PaymentSessionCreateParams
    {
        public PaymentSessionCreateParams(
            double amount,
            string currency,
            string description,
            int? expiresIn = null,
            IReadOnlyList<MetaDataParams>? metaData = null,
            string? invoiceNumber = null,
            string? referenceNumber = null,
            int? maxRetryCount = null,
            ConfigurationsParams? configurations = null)
        {
            if (string.IsNullOrEmpty(currency))
            {
                throw new ArgumentException("currency is required", nameof(currency));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("description is required", nameof(description));
            }

            if (expiresIn.HasValue && (expiresIn.Value < 300 || expiresIn.Value > 900))
            {
                throw new ArgumentException("expires_in must be between 300 and 900 seconds", nameof(expiresIn));
            }

            if (maxRetryCount.HasValue && (maxRetryCount.Value < 1 || maxRetryCount.Value > 5))
            {
                throw new ArgumentException("max_retry_count must be between 1 and 5", nameof(maxRetryCount));
            }

            ParamValidator.ValidateDescription(description);
            ParamValidator.ValidateInvoiceNumber(invoiceNumber);
            ParamValidator.ValidateReferenceNumber(referenceNumber);
            MetaDataValidator.Validate(metaData);

            Amount = amount;
            Currency = currency;
            Description = description;
            ExpiresIn = expiresIn;
            MetaData = metaData;
            InvoiceNumber = invoiceNumber;
            ReferenceNumber = referenceNumber;
            MaxRetryCount = maxRetryCount;
            Configurations = configurations;
        }

        [JsonPropertyName("amount")]
        public double Amount { get; }

        [JsonPropertyName("currency")]
        public string Currency { get; }

        [JsonPropertyName("description")]
        public string Description { get; }

        [JsonPropertyName("expires_in")]
        public int? ExpiresIn { get; }

        [JsonPropertyName("meta_data")]
        public IReadOnlyList<MetaDataParams>? MetaData { get; }

        [JsonPropertyName("invoice_number")]
        public string? InvoiceNumber { get; }

        [JsonPropertyName("reference_number")]
        public string? ReferenceNumber { get; }

        [JsonPropertyName("max_retry_count")]
        public int? MaxRetryCount { get; }

        [JsonPropertyName("configurations")]
        public ConfigurationsParams? Configurations { get; }
    }
}
