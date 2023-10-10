using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace eventz.Utils
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stringValue = reader.GetString();
            string[] formats = { "yyyy-MM-ddTHH:mm:ss.fffZ", "MM/dd/yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss zzz" };

            return DateTime.ParseExact(stringValue, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);

        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss zzz"));
        }
    }
}
