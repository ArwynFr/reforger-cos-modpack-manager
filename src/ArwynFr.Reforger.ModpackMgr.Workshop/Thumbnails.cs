
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record Thumbnails(
    [property: JsonPropertyName("image/jpeg")] IReadOnlyList<ImageJpeg> ImageJpeg
);