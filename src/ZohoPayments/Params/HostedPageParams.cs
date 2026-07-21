using System;
using System.Text.Json.Serialization;

namespace ZohoPayments.Params
{
    /// <summary>Hosted-page configuration. <see cref="Description"/>, <see cref="SuccessUrl"/> and <see cref="FailureUrl"/> are required.</summary>
    public sealed class HostedPageParams
    {
        public HostedPageParams(
            string description,
            string successUrl,
            string failureUrl,
            string? name = null,
            string? email = null,
            string? phone = null,
            string? phoneCountryCode = null,
            string? udf1 = null,
            string? udf2 = null,
            string? udf3 = null,
            string? udf4 = null,
            string? udf5 = null)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("description is required", nameof(description));
            }

            if (string.IsNullOrEmpty(successUrl))
            {
                throw new ArgumentException("success_url is required", nameof(successUrl));
            }

            if (string.IsNullOrEmpty(failureUrl))
            {
                throw new ArgumentException("failure_url is required", nameof(failureUrl));
            }

            Description = description;
            SuccessUrl = successUrl;
            FailureUrl = failureUrl;
            Name = name;
            Email = email;
            Phone = phone;
            PhoneCountryCode = phoneCountryCode;
            Udf1 = udf1;
            Udf2 = udf2;
            Udf3 = udf3;
            Udf4 = udf4;
            Udf5 = udf5;
        }

        [JsonPropertyName("description")]
        public string Description { get; }

        [JsonPropertyName("success_url")]
        public string SuccessUrl { get; }

        [JsonPropertyName("failure_url")]
        public string FailureUrl { get; }

        [JsonPropertyName("name")]
        public string? Name { get; }

        [JsonPropertyName("email")]
        public string? Email { get; }

        [JsonPropertyName("phone")]
        public string? Phone { get; }

        [JsonPropertyName("phone_country_code")]
        public string? PhoneCountryCode { get; }

        [JsonPropertyName("udf1")]
        public string? Udf1 { get; }

        [JsonPropertyName("udf2")]
        public string? Udf2 { get; }

        [JsonPropertyName("udf3")]
        public string? Udf3 { get; }

        [JsonPropertyName("udf4")]
        public string? Udf4 { get; }

        [JsonPropertyName("udf5")]
        public string? Udf5 { get; }
    }
}
