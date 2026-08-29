using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.ServerConfig;

public record ServerConfigurationModEntry(
    [property: JsonPropertyName("modId")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("version")] string Version);