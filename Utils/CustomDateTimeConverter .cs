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
            if (string.IsNullOrEmpty(stringValue))
            {
                throw new JsonException("String value is null or empty.");
            }

            string[] formats = { "yyyy-MM-ddTHH:mm:ss.fffZ", "MM/dd/yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss zzz" };
            try
            {
                return DateTime.ParseExact(stringValue, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            catch (FormatException e)
            {
                throw new JsonException("String value is not in an acceptable DateTime format.", e);
            }
        }


        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss zzz"));
        }
    }
}
