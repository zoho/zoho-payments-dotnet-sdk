using System.Text.Json;

namespace ZohoPayments.Internal
{
    internal sealed class ApiResponse
    {
        public ApiResponse(int statusCode, JsonElement? body)
        {
            StatusCode = statusCode;
            Body = body;
        }

        public int StatusCode { get; }

        public JsonElement? Body { get; }

        public string? GetCodeString()
        {
            if (Body is null || !Body.Value.TryGetProperty("code", out var code))
            {
                return null;
            }

            return code.ValueKind == JsonValueKind.String ? code.GetString() : code.ToString();
        }

        public string? GetMessage()
        {
            if (Body is null || !Body.Value.TryGetProperty("message", out var message))
            {
                return null;
            }

            return message.ValueKind == JsonValueKind.String ? message.GetString() : message.ToString();
        }

        public bool IsSuccess() => StatusCode >= 200 && StatusCode < 300;
    }
}
