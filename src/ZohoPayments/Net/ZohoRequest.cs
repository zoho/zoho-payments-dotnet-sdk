using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ZohoPayments.Internal;

namespace ZohoPayments.Net
{
    /// <summary>Immutable HTTP request passed to an <see cref="IHttpClient"/> transport.</summary>
    public sealed class ZohoRequest
    {
        internal ZohoRequest(
            RequestMethod method,
            string url,
            IDictionary<string, List<string>> headers,
            string? body,
            double? timeout)
        {
            Method = method;
            Url = url;
            var frozen = new Dictionary<string, IReadOnlyList<string>>();
            foreach (var pair in headers)
            {
                frozen[pair.Key] = new List<string>(pair.Value).AsReadOnly();
            }

            Headers = new ReadOnlyDictionary<string, IReadOnlyList<string>>(frozen);
            Body = body;
            Timeout = timeout;
        }

        public RequestMethod Method { get; }

        public string Url { get; }

        public IReadOnlyDictionary<string, IReadOnlyList<string>> Headers { get; }

        public string? Body { get; }

        /// <summary>Per-request timeout in seconds, or null for the transport default.</summary>
        public double? Timeout { get; }

        public static ZohoRequestBuilder Builder() => new ZohoRequestBuilder();

        /// <summary>Renders the request with the <c>Authorization</c> header masked so the OAuth token is never leaked.</summary>
        public override string ToString()
        {
            var headers = new StringBuilder("{");
            var first = true;
            foreach (var pair in Headers)
            {
                if (!first)
                {
                    headers.Append(", ");
                }

                first = false;
                var value = string.Join(", ", pair.Value.Select(headerValue => Redact.MaskHeaderValue(pair.Key, headerValue)));
                headers.Append($"{pair.Key}: {value}");
            }

            headers.Append("}");
            return $"ZohoRequest{{Method: {Method}, Url: {Url}, Headers: {headers}, Body: {Body}}}";
        }
    }

    public sealed class ZohoRequestBuilder
    {
        private RequestMethod? _method;
        private string? _url;
        private readonly Dictionary<string, List<string>> _headers = new Dictionary<string, List<string>>();
        private string? _body;
        private double? _timeout;

        public ZohoRequestBuilder Method(RequestMethod method)
        {
            _method = method;
            return this;
        }

        public ZohoRequestBuilder Url(string url)
        {
            _url = url;
            return this;
        }

        /// <summary>Appends a header value, allowing multiple values per name.</summary>
        public ZohoRequestBuilder Header(string name, string value)
        {
            if (!_headers.TryGetValue(name, out var values))
            {
                values = new List<string>();
                _headers[name] = values;
            }

            values.Add(value);
            return this;
        }

        /// <summary>Replaces any existing values for the header name.</summary>
        public ZohoRequestBuilder SetHeader(string name, string value)
        {
            _headers[name] = new List<string> { value };
            return this;
        }

        public ZohoRequestBuilder Headers(IEnumerable<KeyValuePair<string, string>> headers)
        {
            foreach (var pair in headers)
            {
                Header(pair.Key, pair.Value);
            }

            return this;
        }

        public ZohoRequestBuilder Body(string? body)
        {
            _body = body;
            return this;
        }

        public ZohoRequestBuilder Timeout(double? timeout)
        {
            _timeout = timeout;
            return this;
        }

        public ZohoRequest Build()
        {
            if (_method is null)
            {
                throw new InvalidOperationException("method is required");
            }

            if (string.IsNullOrEmpty(_url))
            {
                throw new InvalidOperationException("url is required");
            }

            return new ZohoRequest(_method.Value, _url!, _headers.ToDictionary(header => header.Key, header => header.Value), _body, _timeout);
        }
    }
}
