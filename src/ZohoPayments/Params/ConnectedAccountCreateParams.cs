using System;
using System.Text.Json.Serialization;
using ZohoPayments.Internal;

namespace ZohoPayments.Params
{
    /// <summary>Bank account details for the connected account being created.</summary>
    public sealed class ConnectedAccountBankAccountParams
    {
        public ConnectedAccountBankAccountParams(string routingNumber, string accountNumber)
        {
            if (string.IsNullOrEmpty(routingNumber))
            {
                throw new ArgumentException("routingNumber is required", nameof(routingNumber));
            }

            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new ArgumentException("accountNumber is required", nameof(accountNumber));
            }

            RoutingNumber = routingNumber;
            AccountNumber = accountNumber;
        }

        [JsonPropertyName("routing_number")]
        public string RoutingNumber { get; }

        [JsonPropertyName("account_number")]
        public string AccountNumber { get; }
    }

    /// <summary>Body for <c>POST /connectedaccounts</c>.</summary>
    public sealed class ConnectedAccountCreateParams
    {
        public ConnectedAccountCreateParams(
            string accountName,
            string emailId,
            string pan,
            string mcc,
            string businessDescription,
            ConnectedAccountBankAccountParams connectedAccountBankAccount)
        {
            if (string.IsNullOrEmpty(accountName))
            {
                throw new ArgumentException("accountName is required", nameof(accountName));
            }

            if (string.IsNullOrEmpty(emailId))
            {
                throw new ArgumentException("emailId is required", nameof(emailId));
            }

            if (string.IsNullOrEmpty(pan))
            {
                throw new ArgumentException("pan is required", nameof(pan));
            }

            if (string.IsNullOrEmpty(mcc))
            {
                throw new ArgumentException("mcc is required", nameof(mcc));
            }

            if (string.IsNullOrEmpty(businessDescription))
            {
                throw new ArgumentException("businessDescription is required", nameof(businessDescription));
            }

            if (connectedAccountBankAccount is null)
            {
                throw new ArgumentException(
                    "connectedAccountBankAccount is required", nameof(connectedAccountBankAccount));
            }

            AccountName = accountName;
            EmailId = emailId;
            Pan = pan;
            Mcc = mcc;
            BusinessDescription = businessDescription;
            ConnectedAccountBankAccount = connectedAccountBankAccount;
        }

        [JsonPropertyName("account_name")]
        public string AccountName { get; }

        [JsonPropertyName("email_id")]
        public string EmailId { get; }

        [JsonPropertyName("pan")]
        public string Pan { get; }

        [JsonPropertyName("mcc")]
        public string Mcc { get; }

        [JsonPropertyName("business_description")]
        public string BusinessDescription { get; }

        [JsonPropertyName("connected_account_bank_account")]
        public ConnectedAccountBankAccountParams ConnectedAccountBankAccount { get; }
    }

    /// <summary>Query parameters for <c>GET /connectedaccounts</c>.</summary>
    public sealed class ConnectedAccountListParams : IPaginationParams
    {
        public ConnectedAccountListParams(
            string? connectedAccountId = null,
            string? filterBy = null,
            string? fromDate = null,
            string? toDate = null,
            int? perPage = null,
            int? page = null)
        {
            ConnectedAccountId = connectedAccountId;
            FilterBy = filterBy;
            FromDate = fromDate;
            ToDate = toDate;
            PerPage = perPage;
            Page = page;
        }

        public string? ConnectedAccountId { get; }

        public string? FilterBy { get; }

        public string? FromDate { get; }

        public string? ToDate { get; }

        public int? PerPage { get; }

        public int? Page { get; }

        internal QueryParams ToQuery() =>
            new QueryParams()
                .Add("connected_account_id", ConnectedAccountId)
                .Add("filter_by", FilterBy)
                .Add("from_date", FromDate)
                .Add("to_date", ToDate)
                .Add("per_page", PerPage)
                .Add("page", Page);
    }

    /// <summary>Query parameters for <c>GET /connectedaccounts/{connected_account_id}/transactions</c>.</summary>
    public sealed class ConnectedAccountTransactionListParams : IPaginationParams
    {
        public ConnectedAccountTransactionListParams(
            string? sortColumn = null,
            string? transactionType = null,
            string? transactionId = null,
            string? filterBy = null,
            string? fromDate = null,
            string? toDate = null,
            string? paymentMethodType = null,
            string? cardBrand = null,
            string? cardType = null,
            int? perPage = null,
            int? page = null)
        {
            SortColumn = sortColumn;
            TransactionType = transactionType;
            TransactionId = transactionId;
            FilterBy = filterBy;
            FromDate = fromDate;
            ToDate = toDate;
            PaymentMethodType = paymentMethodType;
            CardBrand = cardBrand;
            CardType = cardType;
            PerPage = perPage;
            Page = page;
        }

        public string? SortColumn { get; }

        public string? TransactionType { get; }

        public string? TransactionId { get; }

        public string? FilterBy { get; }

        public string? FromDate { get; }

        public string? ToDate { get; }

        public string? PaymentMethodType { get; }

        public string? CardBrand { get; }

        public string? CardType { get; }

        public int? PerPage { get; }

        public int? Page { get; }

        internal QueryParams ToQuery() =>
            new QueryParams()
                .Add("sort_column", SortColumn)
                .Add("transaction_type", TransactionType)
                .Add("transaction_id", TransactionId)
                .Add("filter_by", FilterBy)
                .Add("from_date", FromDate)
                .Add("to_date", ToDate)
                .Add("payment_method_type", PaymentMethodType)
                .Add("card_brand", CardBrand)
                .Add("card_type", CardType)
                .Add("per_page", PerPage)
                .Add("page", Page);
    }
}
