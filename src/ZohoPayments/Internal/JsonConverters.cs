using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZohoPayments.Internal
{

    internal sealed class TolerantStringConverter : JsonConverter<string?>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return null;
                case JsonTokenType.String:
                    return reader.GetString();
                case JsonTokenType.Number:
                case JsonTokenType.True:
                case JsonTokenType.False:
                    using (var document = JsonDocument.ParseValue(ref reader))
                    {
                        return document.RootElement.GetRawText();
                    }

                default:
                    reader.Skip();
                    return null;
            }
        }

        public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteStringValue(value);
            }
        }
    }

    // The API sometimes returns numbers as JSON strings (e.g. "amount_paid": "10.00"), so these
    // converters accept both numbers and numeric strings; blank/unparseable values become null.
    internal sealed class TolerantNullableDoubleConverter : JsonConverter<double?>
    {
        public override double? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return null;
                case JsonTokenType.Number:
                    return reader.GetDouble();
                case JsonTokenType.String:
                    var text = reader.GetString();
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        return null;
                    }

                    return double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsed)
                        ? parsed
                        : (double?)null;
                default:
                    reader.Skip();
                    return null;
            }
        }

        public override void Write(Utf8JsonWriter writer, double? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteNumberValue(value.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }

    internal sealed class TolerantNullableLongConverter : JsonConverter<long?>
    {
        public override long? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return null;
                case JsonTokenType.Number:
                    return reader.TryGetInt64(out var number) ? number : (long?)null;
                case JsonTokenType.String:
                    var text = reader.GetString();
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        return null;
                    }

                    return long.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsed)
                        ? parsed
                        : (long?)null;
                default:
                    reader.Skip();
                    return null;
            }
        }

        public override void Write(Utf8JsonWriter writer, long? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteNumberValue(value.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }

    internal sealed class TolerantNullableIntConverter : JsonConverter<int?>
    {
        public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return null;
                case JsonTokenType.Number:
                    return reader.TryGetInt32(out var number) ? number : (int?)null;
                case JsonTokenType.String:
                    var text = reader.GetString();
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        return null;
                    }

                    return int.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsed)
                        ? parsed
                        : (int?)null;
                default:
                    reader.Skip();
                    return null;
            }
        }

        public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteNumberValue(value.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}
