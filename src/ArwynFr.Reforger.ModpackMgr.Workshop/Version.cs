
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record Version(
    [property: JsonPropertyName("version")] string VersionName,
    [property: JsonPropertyName("approved")] bool? Approved,
    [property: JsonPropertyName("published")] bool? Published,
    [property: JsonPropertyName("gameVersion")] string GameVersion,
    [property: JsonPropertyName("minGameVersion")] int? MinGameVersion,
    [property: JsonPropertyName("platformCompatibility")] int? PlatformCompatibility,
    [property: JsonPropertyName("totalFileSize")] long? TotalFileSize,
    [property: JsonPropertyName("meta")] object Meta,
    [property: JsonPropertyName("milestone")] bool? Milestone,
    [property: JsonPropertyName("createdAt")] DateTime? CreatedAt,
    [property: JsonPropertyName("updatedAt")] DateTime? UpdatedAt,
    [property: JsonPropertyName("assetId")] string AssetId,
    [property: JsonPropertyName("scenariosCount")] int? ScenariosCount,
    [property: JsonPropertyName("dependenciesCount")] int? DependenciesCount
);