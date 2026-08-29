
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record PagePropsSearch(
    [property: JsonPropertyName("search")] string Search,
    [property: JsonPropertyName("page")] int? Page,
    [property: JsonPropertyName("assets")] Assets Assets
);