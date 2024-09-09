using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Unistream.TestTask.BLL.Transactions.Queries.DeserializeTransaction;

public class DateTimeConverter : JsonConverter<DateTime>
{
    private static readonly string _format = "o";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateTime = DateTime.Parse(reader.GetString(), null, DateTimeStyles.RoundtripKind);
        return dateTime;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        var formatted = value.ToString(_format);
        writer.WriteStringValue(formatted);
    }
}
