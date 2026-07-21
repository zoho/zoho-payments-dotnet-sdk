using System;
using ZohoPayments.Auth;
using ZohoPayments.Internal;
using ZohoPayments.Services;

namespace ZohoPayments
{
    public sealed class ZohoPaymentsClient : IDisposable
    {
        /// <summary>Starts building a new <see cref="ZohoPaymentsClient"/>.</summary>
        public static ZohoPaymentsClientBuilder Builder() => new ZohoPaymentsClientBuilder();

        /// <summary>
        /// Generates a new OAuth access token by exchanging the supplied refresh token.
        /// The SDK does not refresh tokens automatically - callers push the new token into a
        /// running client via <see cref="UpdateToken"/>.
        /// </summary>
        public static OAuthToken GenerateAccessToken(
            string refreshToken,
            string clientId,
            string clientSecret,
            string redirectUri,
            Edition edition) =>
            OAuthTokenRefresher.GenerateAccessToken(refreshToken, clientId, clientSecret, redirectUri, edition);

        private readonly ZohoHttpClient _httpClient;
        private readonly TokenManager _tokenManager;
        private readonly Edition _edition;

        private readonly PaymentLinkService _paymentLinks;
        private readonly PaymentSessionService _paymentSessions;
        private readonly CustomerService _customers;
        private readonly PaymentService _payments;
        private readonly RefundService _refunds;
        private readonly PaymentMethodService _paymentMethods;
        private readonly PaymentMethodSessionService _paymentMethodSessions;
        private readonly MandateService _mandates;
        private readonly CollectService _collect;

        private volatile bool _closed;
        private readonly object _closeLock = new object();

        internal ZohoPaymentsClient(ZohoHttpClient httpClient, TokenManager tokenManager, Edition edition)
        {
            _httpClient = httpClient;
            _tokenManager = tokenManager;
            _edition = edition;

            _paymentLinks = new PaymentLinkService(httpClient);
            _paymentSessions = new PaymentSessionService(httpClient);
            _customers = new CustomerService(httpClient, edition);
            _payments = new PaymentService(httpClient, edition);
            _refunds = new RefundService(httpClient);
            _paymentMethods = new PaymentMethodService(httpClient);
            _paymentMethodSessions = new PaymentMethodSessionService(httpClient);
            _mandates = new MandateService(httpClient);
            _collect = new CollectService(httpClient);
        }

        public PaymentLinkService PaymentLinks() => _paymentLinks;

        public PaymentSessionService PaymentSessions() => _paymentSessions;

        public CustomerService Customers() => _customers;

        public PaymentService Payments() => _payments;

        public RefundService Refunds() => _refunds;

        /// <summary>Saved payment methods (<c>/paymentmethods</c>). Requires <see cref="Edition.US"/>.</summary>
        /// <exception cref="System.InvalidOperationException">if this client was built with an India edition.</exception>
        public PaymentMethodService PaymentMethods()
        {
            if (!_edition.IsUs())
            {
                throw new InvalidOperationException("PaymentMethods() is available only on Edition.US");
            }

            return _paymentMethods;
        }

        /// <summary>Payment method sessions (<c>/paymentmethodsessions</c>). Requires <see cref="Edition.US"/>.</summary>
        /// <exception cref="System.InvalidOperationException">if this client was built with an India edition.</exception>
        public PaymentMethodSessionService PaymentMethodSessions()
        {
            if (!_edition.IsUs())
            {
                throw new InvalidOperationException("PaymentMethodSessions() is available only on Edition.US");
            }

            return _paymentMethodSessions;
        }

        /// <summary>Mandates API (<c>/mandates</c>, mandate payment sessions). Requires an India edition.</summary>
        /// <exception cref="System.InvalidOperationException">if this client was built with <see cref="Edition.US"/>.</exception>
        public MandateService Mandates()
        {
            if (!_edition.IsIn())
            {
                throw new InvalidOperationException("Mandates() is available only on Edition.IN / Edition.IN_SANDBOX");
            }

            return _mandates;
        }

        /// <summary>Collect — virtual accounts API (<c>/virtualaccounts</c>). Requires an India edition.</summary>
        /// <exception cref="System.InvalidOperationException">if this client was built with <see cref="Edition.US"/>.</exception>
        public CollectService Collect()
        {
            if (!_edition.IsIn())
            {
                throw new InvalidOperationException("Collect() is available only on Edition.IN / Edition.IN_SANDBOX");
            }

            return _collect;
        }

        /// <summary>
        /// Replaces the active access token for all subsequent API requests.
        /// Thread-safe: in-flight requests complete with the old token.
        /// </summary>
        public void UpdateToken(string newAccessToken) => _tokenManager.UpdateToken(newAccessToken);

        /// <summary>Renders the client without exposing the OAuth token.</summary>
        public override string ToString() => $"ZohoPaymentsClient{{Edition: {_edition}}}";

        /// <summary>Releases resources held by the underlying HTTP transport. Idempotent — safe to call more than once.</summary>
        public void Dispose()
        {
            lock (_closeLock)
            {
                if (_closed)
                {
                    return;
                }

                _closed = true;
            }

            _httpClient.Close();
        }
    }
}
