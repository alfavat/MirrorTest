using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebUI.Helper
{
    public class DateTimeToStringConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

            var value = reader.GetString();
            return DateTime.Parse(value, cultureinfo);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
            writer.WriteStringValue(value.ToString("MM/dd/yyyy HH:mm:ss", cultureinfo));
        }
    }
}
