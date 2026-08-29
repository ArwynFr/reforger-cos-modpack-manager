
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record Tag(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("category")] object Category
);