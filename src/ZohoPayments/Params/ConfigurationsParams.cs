using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZohoPayments.Params
{
    public sealed class ConfigurationsParams
    {
        public ConfigurationsParams(
            IReadOnlyList<string>? allowedPaymentMethods = null,
            HostedPageParams? hostedPageParameters = null)
        {
            AllowedPaymentMethods = allowedPaymentMethods;
            HostedPageParameters = hostedPageParameters;
        }

        [JsonPropertyName("allowed_payment_methods")]
        public IReadOnlyList<string>? AllowedPaymentMethods { get; }

        [JsonPropertyName("hosted_page_parameters")]
        public HostedPageParams? HostedPageParameters { get; }
    }
}
