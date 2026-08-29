
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record DependencyTree(
    [property: JsonPropertyName("access")] string Access,
    [property: JsonPropertyName("assetId")] string AssetId,
    [property: JsonPropertyName("blocked")] bool? Blocked,
    [property: JsonPropertyName("private")] bool? Private,
    [property: JsonPropertyName("version")] string Version,
    [property: JsonPropertyName("unlisted")] bool? Unlisted,
    [property: JsonPropertyName("deletedAt")] object DeletedAt,
    [property: JsonPropertyName("accessValue")] int? AccessValue,
    [property: JsonPropertyName("gameVersion")] string GameVersion,
    [property: JsonPropertyName("assetVersionId")] int? AssetVersionId,
    [property: JsonPropertyName("platformCompatibility")] string PlatformCompatibility,
    [property: JsonPropertyName("platformCompatibilityValue")] int? PlatformCompatibilityValue
);