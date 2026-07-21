using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZohoPayments.Internal;

namespace ZohoPayments.Params
{
    public sealed class CustomerCreateParams
    {
        public CustomerCreateParams(
            string name,
            string email,
            string? phone = null,
            string? phoneCountryCode = null,
            IReadOnlyList<MetaDataParams>? metaData = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name is required", nameof(name));
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("email is required", nameof(email));
            }

            MetaDataValidator.Validate(metaData);

            Name = name;
            Email = email;
            Phone = phone;
            PhoneCountryCode = phoneCountryCode;
            MetaData = metaData;
        }

        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("email")]
        public string Email { get; }

        [JsonPropertyName("phone")]
        public string? Phone { get; }

        [JsonPropertyName("phone_country_code")]
        public string? PhoneCountryCode { get; }

        [JsonPropertyName("meta_data")]
        public IReadOnlyList<MetaDataParams>? MetaData { get; }
    }

    public sealed class CustomerListParams : IPaginationParams
    {
        public CustomerListParams(
            string? filterBy = null,
            string? fromDate = null,
            string? toDate = null,
            int? perPage = null,
            int? page = null)
        {
            FilterBy = filterBy;
            FromDate = fromDate;
            ToDate = toDate;
            PerPage = perPage;
            Page = page;
        }

        public string? FilterBy { get; }

        public string? FromDate { get; }

        public string? ToDate { get; }

        public int? PerPage { get; }

        public int? Page { get; }

        internal QueryParams ToQuery() =>
            new QueryParams()
                .Add("filter_by", FilterBy)
                .Add("from_date", FromDate)
                .Add("to_date", ToDate)
                .Add("per_page", PerPage)
                .Add("page", Page);
    }
}
