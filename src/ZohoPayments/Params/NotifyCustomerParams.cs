using System.Text.Json.Serialization;

namespace ZohoPayments.Params
{
    public sealed class NotifyCustomerParams
    {
        public NotifyCustomerParams(bool? email = null, bool? sms = null)
        {
            Email = email;
            Sms = sms;
        }

        [JsonPropertyName("email")]
        public bool? Email { get; }

        [JsonPropertyName("sms")]
        public bool? Sms { get; }
    }
}
