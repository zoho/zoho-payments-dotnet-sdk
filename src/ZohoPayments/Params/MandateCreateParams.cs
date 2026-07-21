using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZohoPayments.Params
{
    public sealed class MandateDetailsParams
    {
        public MandateDetailsParams(
            string paymentMethodType,
            string frequency,
            string description,
            string amountRule,
            double? maxAmount = null,
            string? startDate = null,
            string? endDate = null,
            int? debitDay = null,
            string? debitRule = null)
        {
            if (string.IsNullOrEmpty(paymentMethodType))
            {
                throw new ArgumentException("payment_method_type is required", nameof(paymentMethodType));
            }

            if (string.IsNullOrEmpty(frequency))
            {
                throw new ArgumentException("frequency is required", nameof(frequency));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("description is required", nameof(description));
            }

            if (string.IsNullOrEmpty(amountRule))
            {
                throw new ArgumentException("amount_rule is required", nameof(amountRule));
            }

            if (amountRule == "variable" && maxAmount is null)
            {
                throw new ArgumentException("max_amount is required when amount_rule is 'variable'", nameof(maxAmount));
            }

            ParamValidator.ValidateDescription(description);

            PaymentMethodType = paymentMethodType;
            Frequency = frequency;
            Description = description;
            AmountRule = amountRule;
            MaxAmount = maxAmount;
            StartDate = startDate;
            EndDate = endDate;
            DebitDay = debitDay;
            DebitRule = debitRule;
        }

        [JsonPropertyName("payment_method_type")]
        public string PaymentMethodType { get; }

        [JsonPropertyName("frequency")]
        public string Frequency { get; }

        [JsonPropertyName("description")]
        public string Description { get; }

        [JsonPropertyName("amount_rule")]
        public string AmountRule { get; }

        [JsonPropertyName("max_amount")]
        public double? MaxAmount { get; }

        [JsonPropertyName("start_date")]
        public string? StartDate { get; }

        [JsonPropertyName("end_date")]
        public string? EndDate { get; }

        [JsonPropertyName("debit_day")]
        public int? DebitDay { get; }

        [JsonPropertyName("debit_rule")]
        public string? DebitRule { get; }
    }

    public sealed class MandateConfigurationsParams
    {
        public MandateConfigurationsParams(HostedPageParams? hostedPageParameters = null)
        {
            HostedPageParameters = hostedPageParameters;
        }

        [JsonPropertyName("hosted_page_parameters")]
        public HostedPageParams? HostedPageParameters { get; }
    }

    public sealed class MandateEnrollmentSessionCreateParams
    {
        public MandateEnrollmentSessionCreateParams(
            double amount,
            string currency,
            string customerId,
            string description,
            MandateDetailsParams mandateDetails,
            string? invoiceNumber = null,
            int? maxRetryCount = null,
            IReadOnlyList<MetaDataParams>? metaData = null,
            MandateConfigurationsParams? configurations = null)
        {
            if (string.IsNullOrEmpty(currency))
            {
                throw new ArgumentException("currency is required", nameof(currency));
            }

            if (string.IsNullOrEmpty(customerId))
            {
                throw new ArgumentException("customer_id is required", nameof(customerId));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("description is required", nameof(description));
            }

            if (mandateDetails is null)
            {
                throw new ArgumentException("mandate_details is required", nameof(mandateDetails));
            }

            if (maxRetryCount.HasValue && (maxRetryCount.Value < 1 || maxRetryCount.Value > 3))
            {
                throw new ArgumentException("max_retry_count must be between 1 and 3", nameof(maxRetryCount));
            }

            ParamValidator.ValidateDescription(description);
            ParamValidator.ValidateInvoiceNumber(invoiceNumber);
            MetaDataValidator.Validate(metaData);

            Amount = amount;
            Currency = currency;
            CustomerId = customerId;
            Description = description;
            MandateDetails = mandateDetails;
            InvoiceNumber = invoiceNumber;
            MaxRetryCount = maxRetryCount;
            MetaData = metaData;
            Configurations = configurations;
        }

        [JsonPropertyName("type")]
        public string Type => "mandate_enrollment";

        [JsonPropertyName("amount")]
        public double Amount { get; }

        [JsonPropertyName("currency")]
        public string Currency { get; }

        [JsonPropertyName("customer_id")]
        public string CustomerId { get; }

        [JsonPropertyName("description")]
        public string Description { get; }

        [JsonPropertyName("mandate_details")]
        public MandateDetailsParams MandateDetails { get; }

        [JsonPropertyName("invoice_number")]
        public string? InvoiceNumber { get; }

        [JsonPropertyName("max_retry_count")]
        public int? MaxRetryCount { get; }

        [JsonPropertyName("meta_data")]
        public IReadOnlyList<MetaDataParams>? MetaData { get; }

        [JsonPropertyName("configurations")]
        public MandateConfigurationsParams? Configurations { get; }
    }

    public sealed class MandateExecutionSessionCreateParams
    {
        public MandateExecutionSessionCreateParams(
            double amount,
            string currency,
            string customerId,
            string description,
            string invoiceNumber,
            int? maxRetryCount = null,
            IReadOnlyList<MetaDataParams>? metaData = null)
        {
            if (string.IsNullOrEmpty(currency))
            {
                throw new ArgumentException("currency is required", nameof(currency));
            }

            if (string.IsNullOrEmpty(customerId))
            {
                throw new ArgumentException("customer_id is required", nameof(customerId));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("description is required", nameof(description));
            }

            if (string.IsNullOrEmpty(invoiceNumber))
            {
                throw new ArgumentException("invoice_number is required", nameof(invoiceNumber));
            }

            if (maxRetryCount.HasValue && (maxRetryCount.Value < 1 || maxRetryCount.Value > 3))
            {
                throw new ArgumentException("max_retry_count must be between 1 and 3", nameof(maxRetryCount));
            }

            ParamValidator.ValidateDescription(description);
            ParamValidator.ValidateInvoiceNumber(invoiceNumber);
            MetaDataValidator.Validate(metaData);

            Amount = amount;
            Currency = currency;
            CustomerId = customerId;
            Description = description;
            InvoiceNumber = invoiceNumber;
            MaxRetryCount = maxRetryCount;
            MetaData = metaData;
        }

        [JsonPropertyName("type")]
        public string Type => "mandate_execution";

        [JsonPropertyName("amount")]
        public double Amount { get; }

        [JsonPropertyName("currency")]
        public string Currency { get; }

        [JsonPropertyName("customer_id")]
        public string CustomerId { get; }

        [JsonPropertyName("description")]
        public string Description { get; }

        [JsonPropertyName("invoice_number")]
        public string InvoiceNumber { get; }

        [JsonPropertyName("max_retry_count")]
        public int? MaxRetryCount { get; }

        [JsonPropertyName("meta_data")]
        public IReadOnlyList<MetaDataParams>? MetaData { get; }
    }

    public sealed class MandateNotifyParams
    {
        public MandateNotifyParams(
            string mandateId,
            double amount,
            string executionDate,
            string description,
            string invoiceNumber)
        {
            if (string.IsNullOrEmpty(mandateId))
            {
                throw new ArgumentException("mandate_id is required", nameof(mandateId));
            }

            if (string.IsNullOrEmpty(executionDate))
            {
                throw new ArgumentException("execution_date is required", nameof(executionDate));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("description is required", nameof(description));
            }

            if (string.IsNullOrEmpty(invoiceNumber))
            {
                throw new ArgumentException("invoice_number is required", nameof(invoiceNumber));
            }

            ParamValidator.ValidateDescription(description);
            ParamValidator.ValidateInvoiceNumber(invoiceNumber);

            MandateId = mandateId;
            Amount = amount;
            ExecutionDate = executionDate;
            Description = description;
            InvoiceNumber = invoiceNumber;
        }

        [JsonPropertyName("mandate_id")]
        public string MandateId { get; }

        [JsonPropertyName("amount")]
        public double Amount { get; }

        [JsonPropertyName("execution_date")]
        public string ExecutionDate { get; }

        [JsonPropertyName("description")]
        public string Description { get; }

        [JsonPropertyName("invoice_number")]
        public string InvoiceNumber { get; }
    }

    public sealed class MandateExecuteParams
    {
        public MandateExecuteParams(
            string customerId,
            string mandateId,
            string paymentsSessionId,
            string invoiceNumber,
            double amount,
            string? mandateNotificationId = null,
            string? receiptEmail = null,
            string? phone = null,
            string? phoneCountryCode = null,
            string? description = null,
            string? referenceNumber = null)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                throw new ArgumentException("customer_id is required", nameof(customerId));
            }

            if (string.IsNullOrEmpty(mandateId))
            {
                throw new ArgumentException("mandate_id is required", nameof(mandateId));
            }

            if (string.IsNullOrEmpty(paymentsSessionId))
            {
                throw new ArgumentException("payments_session_id is required", nameof(paymentsSessionId));
            }

            if (string.IsNullOrEmpty(invoiceNumber))
            {
                throw new ArgumentException("invoice_number is required", nameof(invoiceNumber));
            }

            ParamValidator.ValidateDescription(description);
            ParamValidator.ValidateInvoiceNumber(invoiceNumber);
            ParamValidator.ValidateReferenceNumber(referenceNumber);

            CustomerId = customerId;
            MandateId = mandateId;
            PaymentsSessionId = paymentsSessionId;
            InvoiceNumber = invoiceNumber;
            Amount = amount;
            MandateNotificationId = mandateNotificationId;
            ReceiptEmail = receiptEmail;
            Phone = phone;
            PhoneCountryCode = phoneCountryCode;
            Description = description;
            ReferenceNumber = referenceNumber;
        }

        [JsonPropertyName("customer_id")]
        public string CustomerId { get; }

        [JsonPropertyName("mandate_id")]
        public string MandateId { get; }

        [JsonPropertyName("payments_session_id")]
        public string PaymentsSessionId { get; }

        [JsonPropertyName("invoice_number")]
        public string InvoiceNumber { get; }

        [JsonPropertyName("amount")]
        public double Amount { get; }

        [JsonPropertyName("mandate_notification_id")]
        public string? MandateNotificationId { get; }

        [JsonPropertyName("receipt_email")]
        public string? ReceiptEmail { get; }

        [JsonPropertyName("phone")]
        public string? Phone { get; }

        [JsonPropertyName("phone_country_code")]
        public string? PhoneCountryCode { get; }

        [JsonPropertyName("description")]
        public string? Description { get; }

        [JsonPropertyName("reference_number")]
        public string? ReferenceNumber { get; }
    }
}
