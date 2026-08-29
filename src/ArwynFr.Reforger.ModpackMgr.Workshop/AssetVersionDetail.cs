
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record AssetVersionDetail(
    [property: JsonPropertyName("changelog")] string Changelog,
    [property: JsonPropertyName("scenarios")] IReadOnlyList<object> Scenarios,
    [property: JsonPropertyName("dependencies")] IReadOnlyList<Dependency> Dependencies
);