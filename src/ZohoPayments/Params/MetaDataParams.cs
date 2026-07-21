using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZohoPayments.Params
{
    /// <summary>Immutable metadata key-value pair.</summary>
    public sealed class MetaDataParams
    {
        public MetaDataParams(string key, string? value = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("meta_data key must not be null or empty", nameof(key));
            }

            Key = key;
            Value = value;
        }

        [JsonPropertyName("key")]
        public string Key { get; }

        [JsonPropertyName("value")]
        public string? Value { get; }
    }

    internal static class MetaDataValidator
    {
        public const int MaxEntries = 5;
        public const int MaxKeyLength = 20;
        public const int MaxValueLength = 500;

        public static void Validate(IReadOnlyList<MetaDataParams>? metaData)
        {
            if (metaData is null)
            {
                return;
            }

            if (metaData.Count > MaxEntries)
            {
                throw new ArgumentException($"meta_data can have at most {MaxEntries} entries");
            }

            foreach (var entry in metaData)
            {
                if (entry is null)
                {
                    throw new ArgumentException("meta_data entry must not be null");
                }

                if (string.IsNullOrEmpty(entry.Key))
                {
                    throw new ArgumentException("meta_data key must not be null or empty");
                }

                if (entry.Key.Length > MaxKeyLength)
                {
                    throw new ArgumentException($"meta_data key must be at most {MaxKeyLength} characters");
                }

                if (entry.Value != null && entry.Value.Length > MaxValueLength)
                {
                    throw new ArgumentException($"meta_data value must be at most {MaxValueLength} characters");
                }
            }
        }
    }
}
