using System;
using ZohoPayments.Internal;
using ZohoPayments.Models;
using ZohoPayments.Params;

namespace ZohoPayments.Services
{
    /// <summary>Zoho Payments Customers API (<c>/customers</c>).</summary>
    public sealed class CustomerService
    {
        private const string SingleEnvelope = "customer";
        private const string ListEnvelope = "customers";

        private readonly ZohoHttpClient _http;
        private readonly Edition _edition;

        internal CustomerService(ZohoHttpClient http, Edition edition)
        {
            _http = http;
            _edition = edition;
        }

        public Customer Create(CustomerCreateParams parameters) =>
            _http.PostObject<Customer>("/customers", parameters, SingleEnvelope);

        public Customer Get(string customerId)
        {
            var path = $"/customers/{ZohoHttpClient.EncodePath(customerId)}";
            return _http.GetObject<Customer>(path, SingleEnvelope);
        }

        /// <summary>Requires <see cref="Edition.US"/>.</summary>
        public ListResponse<CustomerSummary> List(CustomerListParams? parameters = null)
        {
            if (!_edition.IsUs())
            {
                throw new InvalidOperationException("customers.List() is available only on Edition.US");
            }

            var query = parameters?.ToQuery();
            return _http.ListObjects<CustomerSummary>("/customers", query, ListEnvelope);
        }

        /// <summary>Requires <see cref="Edition.US"/>.</summary>
        public void Delete(string customerId)
        {
            if (!_edition.IsUs())
            {
                throw new InvalidOperationException("customers.Delete() is available only on Edition.US");
            }

            var path = $"/customers/{ZohoHttpClient.EncodePath(customerId)}";
            _http.Delete(path);
        }
    }
}
