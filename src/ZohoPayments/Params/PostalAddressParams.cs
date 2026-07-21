using System;
using System.Text.Json.Serialization;

namespace ZohoPayments.Params
{
    /// <summary>Postal address. <see cref="Country"/> is required.</summary>
    public sealed class PostalAddressParams
    {
        public PostalAddressParams(
            string country,
            string? name = null,
            string? addressLine1 = null,
            string? addressLine2 = null,
            string? city = null,
            string? state = null,
            string? postalCode = null)
        {
            if (string.IsNullOrEmpty(country))
            {
                throw new ArgumentException("country is required", nameof(country));
            }

            Country = country;
            Name = name;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            City = city;
            State = state;
            PostalCode = postalCode;
        }

        [JsonPropertyName("name")]
        public string? Name { get; }

        [JsonPropertyName("address_line1")]
        public string? AddressLine1 { get; }

        [JsonPropertyName("address_line2")]
        public string? AddressLine2 { get; }

        [JsonPropertyName("city")]
        public string? City { get; }

        [JsonPropertyName("state")]
        public string? State { get; }

        [JsonPropertyName("country")]
        public string Country { get; }

        [JsonPropertyName("postal_code")]
        public string? PostalCode { get; }
    }
}
