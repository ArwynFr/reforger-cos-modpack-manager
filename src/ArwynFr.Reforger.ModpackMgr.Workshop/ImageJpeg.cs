
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record ImageJpeg(
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("width")] int? Width,
    [property: JsonPropertyName("height")] int? Height,
    [property: JsonPropertyName("contentType")] string ContentType
);