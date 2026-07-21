using System;
using System.Collections.Generic;
using System.Text.Json;
using ZohoPayments.Exceptions;
using ZohoPayments.Models;

namespace ZohoPayments.Internal
{
    // JSON helpers: null-stripping serialization and envelope unwrapping.
    internal static class JsonUtil
    {
        private static readonly JsonSerializerOptions SerializeOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        // Shared with ZohoHttpClient so list items and single objects deserialize the same way.
        public static readonly JsonSerializerOptions DeserializeOptions = CreateDeserializeOptions();

        private static JsonSerializerOptions CreateDeserializeOptions()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new TolerantNullableDoubleConverter());
            options.Converters.Add(new TolerantNullableLongConverter());
            options.Converters.Add(new TolerantNullableIntConverter());
            return options;
        }

        // Serializes an object to JSON, dropping null-valued members.
        public static string ToJson(object value) => JsonSerializer.Serialize(value, value.GetType(), SerializeOptions);

        // Parses a JSON string, requiring a JSON object at the root.
        public static JsonElement ParseObject(string text)
        {
            JsonElement root;
            try
            {
                using var document = JsonDocument.Parse(text);
                root = document.RootElement.Clone();
            }
            catch (JsonException exc)
            {
                throw new ZohoPaymentsException($"Invalid JSON in response: {exc.Message}", exc);
            }

            if (root.ValueKind != JsonValueKind.Object)
            {
                throw new ZohoPaymentsException("Expected JSON object in response");
            }

            return root;
        }

        // Returns the first object value found under any of the keys, or null.
        public static JsonElement? GetObject(JsonElement? body, params string[] keys)
        {
            if (body is null)
            {
                return null;
            }

            foreach (var key in keys)
            {
                if (body.Value.TryGetProperty(key, out var value) && value.ValueKind == JsonValueKind.Object)
                {
                    return value;
                }
            }

            return null;
        }

        public static JsonElement GetObjectRequired(JsonElement? body, params string[] keys)
        {
            var envelope = GetObject(body, keys);
            if (envelope is null)
            {
                throw new ZohoPaymentsException($"Expected JSON object under one of keys: {string.Join(", ", keys)}");
            }

            return envelope.Value;
        }

        // Returns the first array value found under any of the keys.
        public static List<JsonElement> ListFromBody(JsonElement? body, params string[] keys)
        {
            var result = new List<JsonElement>();
            if (body is null)
            {
                return result;
            }

            foreach (var key in keys)
            {
                if (body.Value.TryGetProperty(key, out var value) && value.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in value.EnumerateArray())
                    {
                        result.Add(item);
                    }

                    return result;
                }
            }

            return result;
        }

        // Unwraps the single-resource envelope and deserializes it into T.
        public static T Unwrap<T>(JsonElement? body, params string[] candidateKeys)
        {
            var inner = GetObjectRequired(body, candidateKeys);
            var result = inner.Deserialize<T>(DeserializeOptions);
            if (result is null)
            {
                throw new ZohoPaymentsException($"Failed to deserialize {typeof(T).Name} from response");
            }

            return result;
        }

        public static PageContext ReadPageContext(JsonElement? body)
        {
            var pageContext = GetObject(body, "page_context");
            return pageContext is null ? new PageContext() : PageContext.FromJson(pageContext.Value);
        }
    }
}
