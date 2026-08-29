
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record Preview(
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("width")] int? Width,
    [property: JsonPropertyName("height")] int? Height,
    [property: JsonPropertyName("thumbnails")] Thumbnails Thumbnails,
    [property: JsonPropertyName("contentType")] string ContentType
);