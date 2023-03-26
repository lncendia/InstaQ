using InstaQ.Infrastructure.InstagramRequests.Models;
using Newtonsoft.Json;

namespace InstaQ.Infrastructure.InstagramRequests.Converters;

public class MediaResponseConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        reader.Read();
        reader.Read();
        var conv = serializer.Converters.First(x => x.GetType() == GetType());
        serializer.Converters.Remove(conv);
        var x = serializer.Deserialize<MediaModel>(reader);
        serializer.Converters.Add(conv);
        reader.Read();
        return x!;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(MediaModel);
    }
}