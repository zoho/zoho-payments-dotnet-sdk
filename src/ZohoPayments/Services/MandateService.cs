using ZohoPayments.Internal;
using ZohoPayments.Models;
using ZohoPayments.Params;

namespace ZohoPayments.Services
{
    /// <summary>Zoho Payments Mandates API (<c>/mandates/*</c>). Requires <see cref="Edition.IN"/>.</summary>
    public sealed class MandateService
    {
        private const string SessionEnvelope = "payments_session";
        private const string NotificationEnvelope = "mandate_notification";
        private const string PaymentEnvelope = "payment";
        private const string MandateEnvelope = "mandate";

        private readonly ZohoHttpClient _http;

        internal MandateService(ZohoHttpClient http)
        {
            _http = http;
        }

        public PaymentSession CreateEnrollmentSession(MandateEnrollmentSessionCreateParams parameters) =>
            _http.PostObject<PaymentSession>("/paymentsessions", parameters, SessionEnvelope);

        public PaymentSession CreateExecutionSession(MandateExecutionSessionCreateParams parameters) =>
            _http.PostObject<PaymentSession>("/paymentsessions", parameters, SessionEnvelope);

        public MandateNotification SendNotification(MandateNotifyParams parameters) =>
            _http.PostObject<MandateNotification>("/mandates/notify", parameters, NotificationEnvelope);

        public MandatePayment Execute(MandateExecuteParams parameters) =>
            _http.PostObject<MandatePayment>("/mandates/execute", parameters, PaymentEnvelope);

        public MandateNotification GetNotification(string mandateNotificationId)
        {
            var path = $"/mandates/notifications/{ZohoHttpClient.EncodePath(mandateNotificationId)}";
            return _http.GetObject<MandateNotification>(path, NotificationEnvelope);
        }

        public Mandate Get(string mandateId)
        {
            var path = $"/mandates/{ZohoHttpClient.EncodePath(mandateId)}";
            return _http.GetObject<Mandate>(path, MandateEnvelope);
        }
    }
}
