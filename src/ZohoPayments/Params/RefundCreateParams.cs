using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZohoPayments.Params
{
    public sealed class RefundCreateParams
    {
        public RefundCreateParams(
            double amount,
            string reason,
            string type,
            string? description = null,
            IReadOnlyList<MetaDataParams>? metaData = null)
        {
            if (string.IsNullOrEmpty(reason))
            {
                throw new ArgumentException("reason is required", nameof(reason));
            }

            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("type is required", nameof(type));
            }

            ParamValidator.ValidateDescription(description);
            MetaDataValidator.Validate(metaData);

            Amount = amount;
            Reason = reason;
            Type = type;
            Description = description;
            MetaData = metaData;
        }

        [JsonPropertyName("amount")]
        public double Amount { get; }

        [JsonPropertyName("reason")]
        public string Reason { get; }

        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("description")]
        public string? Description { get; }

        [JsonPropertyName("meta_data")]
        public IReadOnlyList<MetaDataParams>? MetaData { get; }
    }
}
