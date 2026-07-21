using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZohoPayments.Internal;

namespace ZohoPayments.Params
{
    public sealed class VirtualAccountCreateParams
    {
        public VirtualAccountCreateParams(
            string description,
            string? customerId = null,
            double? minimumAmount = null,
            double? maximumAmount = null,
            string? expiresAt = null,
            string? referenceNumber = null,
            IReadOnlyList<MetaDataParams>? metaData = null)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("description is required", nameof(description));
            }

            ParamValidator.ValidateDescription(description);
            ParamValidator.ValidateReferenceNumber(referenceNumber);
            MetaDataValidator.Validate(metaData);

            Description = description;
            CustomerId = customerId;
            MinimumAmount = minimumAmount;
            MaximumAmount = maximumAmount;
            ExpiresAt = expiresAt;
            ReferenceNumber = referenceNumber;
            MetaData = metaData;
        }

        [JsonPropertyName("description")]
        public string Description { get; }

        [JsonPropertyName("customer_id")]
        public string? CustomerId { get; }

        [JsonPropertyName("minimum_amount")]
        public double? MinimumAmount { get; }

        [JsonPropertyName("maximum_amount")]
        public double? MaximumAmount { get; }

        [JsonPropertyName("expires_at")]
        public string? ExpiresAt { get; }

        [JsonPropertyName("reference_number")]
        public string? ReferenceNumber { get; }

        [JsonPropertyName("meta_data")]
        public IReadOnlyList<MetaDataParams>? MetaData { get; }
    }

    /// <summary>All fields are optional, but at least one must be provided.</summary>
    public sealed class VirtualAccountUpdateParams
    {
        public VirtualAccountUpdateParams(
            string? description = null,
            double? minimumAmount = null,
            double? maximumAmount = null,
            string? expiresAt = null,
            string? referenceNumber = null,
            IReadOnlyList<MetaDataParams>? metaData = null)
        {
            ParamValidator.RequireAnyField(description, minimumAmount, maximumAmount, expiresAt, referenceNumber, metaData);
            ParamValidator.ValidateDescription(description);
            ParamValidator.ValidateReferenceNumber(referenceNumber);
            MetaDataValidator.Validate(metaData);

            Description = description;
            MinimumAmount = minimumAmount;
            MaximumAmount = maximumAmount;
            ExpiresAt = expiresAt;
            ReferenceNumber = referenceNumber;
            MetaData = metaData;
        }

        [JsonPropertyName("description")]
        public string? Description { get; }

        [JsonPropertyName("minimum_amount")]
        public double? MinimumAmount { get; }

        [JsonPropertyName("maximum_amount")]
        public double? MaximumAmount { get; }

        [JsonPropertyName("expires_at")]
        public string? ExpiresAt { get; }

        [JsonPropertyName("reference_number")]
        public string? ReferenceNumber { get; }

        [JsonPropertyName("meta_data")]
        public IReadOnlyList<MetaDataParams>? MetaData { get; }
    }

    public sealed class VirtualAccountPaymentListParams : IPaginationParams
    {
        public VirtualAccountPaymentListParams(
            string? status = null,
            string? sortColumn = null,
            string? sortOrder = null,
            int? perPage = null,
            int? page = null)
        {
            Status = status;
            SortColumn = sortColumn;
            SortOrder = sortOrder;
            PerPage = perPage;
            Page = page;
        }

        public string? Status { get; }

        public string? SortColumn { get; }

        public string? SortOrder { get; }

        public int? PerPage { get; }

        public int? Page { get; }

        internal QueryParams ToQuery() =>
            new QueryParams()
                .Add("status", Status)
                .Add("sort_column", SortColumn)
                .Add("sort_order", SortOrder)
                .Add("per_page", PerPage)
                .Add("page", Page);
    }
}
