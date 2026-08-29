
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record Dependency(
    [property: JsonPropertyName("dependencies")] IReadOnlyList<object> Dependencies,
    [property: JsonPropertyName("version")] string Version,
    [property: JsonPropertyName("blocked")] bool? Blocked,
    [property: JsonPropertyName("private")] bool? Private,
    [property: JsonPropertyName("published")] bool? Published,
    [property: JsonPropertyName("totalFileSize")] long? TotalFileSize,
    [property: JsonPropertyName("asset")] Asset Asset
);