using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ZohoPayments.Internal
{
    /// <summary>Ordered, URL-encoded, repeat-tolerant query string builder. Null values are silently dropped.</summary>
    public sealed class QueryParams
    {
        private readonly List<KeyValuePair<string, string>> _entries = new List<KeyValuePair<string, string>>();

        public QueryParams Add(string key, string? value)
        {
            if (value != null)
            {
                _entries.Add(new KeyValuePair<string, string>(key, value));
            }

            return this;
        }

        public QueryParams Add(string key, int? value)
        {
            if (value.HasValue)
            {
                _entries.Add(new KeyValuePair<string, string>(key, value.Value.ToString(CultureInfo.InvariantCulture)));
            }

            return this;
        }

        public QueryParams Add(string key, bool? value)
        {
            if (value.HasValue)
            {
                _entries.Add(new KeyValuePair<string, string>(key, value.Value ? "true" : "false"));
            }

            return this;
        }

        public QueryParams AddAll(QueryParams? other)
        {
            if (other != null)
            {
                _entries.AddRange(other._entries);
            }

            return this;
        }

        public bool IsEmpty => _entries.Count == 0;

        public string ToQueryString()
        {
            var builder = new StringBuilder();
            for (var index = 0; index < _entries.Count; index++)
            {
                if (index > 0)
                {
                    builder.Append('&');
                }

                builder.Append(System.Uri.EscapeDataString(_entries[index].Key));
                builder.Append('=');
                builder.Append(System.Uri.EscapeDataString(_entries[index].Value));
            }

            return builder.ToString();
        }
    }
}
