
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record Asset(
    [property: JsonPropertyName("averageRating")] decimal? AverageRating,
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("summary")] string Summary,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("license")] string License,
    [property: JsonPropertyName("licenseText")] object LicenseText,
    [property: JsonPropertyName("unlisted")] bool? Unlisted,
    [property: JsonPropertyName("private")] bool? Private,
    [property: JsonPropertyName("blocked")] bool? Blocked,
    [property: JsonPropertyName("ratingCount")] int? RatingCount,
    [property: JsonPropertyName("subscriberCount")] int? SubscriberCount,
    [property: JsonPropertyName("currentVersionNumber")] string CurrentVersionNumber,
    [property: JsonPropertyName("currentVersionSize")] long? CurrentVersionSize,
    [property: JsonPropertyName("previews")] IReadOnlyList<Preview> Previews,
    [property: JsonPropertyName("screenshots")] IReadOnlyList<object> Screenshots,
    [property: JsonPropertyName("meta")] object Meta,
    [property: JsonPropertyName("createdAt")] DateTime? CreatedAt,
    [property: JsonPropertyName("updatedAt")] DateTime? UpdatedAt,
    [property: JsonPropertyName("currentVersionId")] int? CurrentVersionId,
    [property: JsonPropertyName("author")] Author Author,
    [property: JsonPropertyName("contributors")] IReadOnlyList<object> Contributors,
    [property: JsonPropertyName("tags")] IReadOnlyList<Tag> Tags,
    [property: JsonPropertyName("dependencyTree")] DependencyTree DependencyTree,
    [property: JsonPropertyName("reported")] bool? Reported,
    [property: JsonPropertyName("ratings")] Ratings Ratings,
    [property: JsonPropertyName("versions")] IReadOnlyList<Version> Versions,
    [property: JsonPropertyName("gameVersion")] string GameVersion,
    [property: JsonPropertyName("obsolete")] bool? Obsolete,
    [property: JsonPropertyName("scenarios")] IReadOnlyList<object> Scenarios,
    [property: JsonPropertyName("dependencies")] IReadOnlyList<Dependency> Dependencies
);