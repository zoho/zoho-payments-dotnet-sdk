using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZohoPayments.Internal;

namespace ZohoPayments.Params
{
    /// <summary>Browser metadata for 3DS / customer-on-session flows.</summary>
    public sealed class BrowserInfo
    {
        public BrowserInfo(
            string? userAgent = null,
            string? acceptHeader = null,
            int? screenHeight = null,
            int? screenWidth = null,
            string? language = null,
            int? timeZoneOffset = null,
            int? colorDepth = null)
        {
            UserAgent = userAgent;
            AcceptHeader = acceptHeader;
            ScreenHeight = screenHeight;
            ScreenWidth = screenWidth;
            Language = language;
            TimeZoneOffset = timeZoneOffset;
            ColorDepth = colorDepth;
        }

        [JsonPropertyName("user_agent")]
        public string? UserAgent { get; }

        [JsonPropertyName("accept_header")]
        public string? AcceptHeader { get; }

        [JsonPropertyName("screen_height")]
        public int? ScreenHeight { get; }

        [JsonPropertyName("screen_width")]
        public int? ScreenWidth { get; }

        [JsonPropertyName("language")]
        public string? Language { get; }

        [JsonPropertyName("time_zone_offset")]
        public int? TimeZoneOffset { get; }

        [JsonPropertyName("color_depth")]
        public int? ColorDepth { get; }
    }

    /// <summary>Payment creation params (US edition only).</summary>
    public sealed class PaymentCreateParams
    {
        public PaymentCreateParams(
            string customerId,
            string paymentMethodId,
            double amount,
            string currency,
            bool? customerOnSession = null,
            BrowserInfo? browserInfo = null,
            string? statementDescriptor = null,
            string? description = null,
            PostalAddressParams? shippingAddress = null,
            IReadOnlyList<MetaDataParams>? metaData = null)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                throw new ArgumentException("customer_id is required", nameof(customerId));
            }

            if (string.IsNullOrEmpty(paymentMethodId))
            {
                throw new ArgumentException("payment_method_id is required", nameof(paymentMethodId));
            }

            if (string.IsNullOrEmpty(currency))
            {
                throw new ArgumentException("currency is required", nameof(currency));
            }

            ParamValidator.ValidateDescription(description);
            MetaDataValidator.Validate(metaData);

            CustomerId = customerId;
            PaymentMethodId = paymentMethodId;
            Amount = amount;
            Currency = currency;
            CustomerOnSession = customerOnSession;
            BrowserInfo = browserInfo;
            StatementDescriptor = statementDescriptor;
            Description = description;
            ShippingAddress = shippingAddress;
            MetaData = metaData;
        }

        [JsonPropertyName("customer_id")]
        public string CustomerId { get; }

        [JsonPropertyName("payment_method_id")]
        public string PaymentMethodId { get; }

        [JsonPropertyName("amount")]
        public double Amount { get; }

        [JsonPropertyName("currency")]
        public string Currency { get; }

        [JsonPropertyName("customer_on_session")]
        public bool? CustomerOnSession { get; }

        [JsonPropertyName("browser_info")]
        public BrowserInfo? BrowserInfo { get; }

        [JsonPropertyName("statement_descriptor")]
        public string? StatementDescriptor { get; }

        [JsonPropertyName("description")]
        public string? Description { get; }

        [JsonPropertyName("shipping_address")]
        public PostalAddressParams? ShippingAddress { get; }

        [JsonPropertyName("meta_data")]
        public IReadOnlyList<MetaDataParams>? MetaData { get; }
    }

    public sealed class PaymentListParams : IPaginationParams
    {
        public PaymentListParams(
            string? status = null,
            string? searchText = null,
            string? filterBy = null,
            string? fromDate = null,
            string? toDate = null,
            string? paymentMethodType = null,
            int? perPage = null,
            int? page = null)
        {
            Status = status;
            SearchText = searchText;
            FilterBy = filterBy;
            FromDate = fromDate;
            ToDate = toDate;
            PaymentMethodType = paymentMethodType;
            PerPage = perPage;
            Page = page;
        }

        public string? Status { get; }

        public string? SearchText { get; }

        public string? FilterBy { get; }

        public string? FromDate { get; }

        public string? ToDate { get; }

        public string? PaymentMethodType { get; }

        public int? PerPage { get; }

        public int? Page { get; }

        internal QueryParams ToQuery() =>
            new QueryParams()
                .Add("status", Status)
                .Add("search_text", SearchText)
                .Add("filter_by", FilterBy)
                .Add("from_date", FromDate)
                .Add("to_date", ToDate)
                .Add("payment_method_type", PaymentMethodType)
                .Add("per_page", PerPage)
                .Add("page", Page);
    }
}
