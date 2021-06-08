using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebMobileUI.Helper
{
    public class NullableDateTimeToStringConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

            var value = reader.GetString();
            if (string.IsNullOrEmpty(value)) return null;
            return DateTime.Parse(value, cultureinfo);
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
            if (!value.HasValue) writer.WriteStringValue("");
            else writer.WriteStringValue(value.Value.ToString("MM/dd/yyyy HH:mm:ss", cultureinfo));
        }
    }
}
