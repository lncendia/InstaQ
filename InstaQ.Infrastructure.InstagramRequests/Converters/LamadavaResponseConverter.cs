using Newtonsoft.Json;

namespace InstaQ.Infrastructure.InstagramRequests.Converters;

public class LamadavaResponseConverter<T> : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        reader.Read();
        var x = serializer.Deserialize<List<T>>(reader);
        var data = reader.ReadAsString();
        reader.Read();
        return (x, data);
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof((List<T>, string?));
    }
}