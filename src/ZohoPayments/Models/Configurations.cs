using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZohoPayments.Models
{
    public sealed class Configurations
    {
        [JsonPropertyName("allowed_payment_methods")]
        public List<string> AllowedPaymentMethods { get; set; } = new List<string>();

        [JsonPropertyName("hosted_page_parameters")]
        public HostedPageResponse? HostedPageParameters { get; set; }
    }
}
