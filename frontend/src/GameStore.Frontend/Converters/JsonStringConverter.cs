using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameStore.Frontend.Converters
{
    public class JsonStringConverter : JsonConverter<string?>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.TokenType == JsonTokenType.Number ? reader.GetInt32().ToString() : reader.GetString();

        public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options) 
            => writer.WriteStringValue(value);
    }
}