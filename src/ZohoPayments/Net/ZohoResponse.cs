using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ZohoPayments.Net
{
    /// <summary>Immutable HTTP response returned by an <see cref="IHttpClient"/> transport.</summary>
    public sealed class ZohoResponse
    {
        public ZohoResponse(int statusCode, IDictionary<string, List<string>> headers, string? body)
        {
            StatusCode = statusCode;
            var frozen = new Dictionary<string, IReadOnlyList<string>>();
            foreach (var pair in headers)
            {
                frozen[pair.Key] = new List<string>(pair.Value).AsReadOnly();
            }

            Headers = new ReadOnlyDictionary<string, IReadOnlyList<string>>(frozen);
            Body = body;
        }

        public int StatusCode { get; }

        public IReadOnlyDictionary<string, IReadOnlyList<string>> Headers { get; }

        public string? Body { get; }
    }
}
