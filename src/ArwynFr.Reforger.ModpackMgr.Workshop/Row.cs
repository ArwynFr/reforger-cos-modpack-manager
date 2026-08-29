
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record Row(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("summary")] string Summary,
    [property: JsonPropertyName("unlisted")] bool? Unlisted,
    [property: JsonPropertyName("private")] bool? Private,
    [property: JsonPropertyName("blocked")] bool? Blocked,
    [property: JsonPropertyName("averageRating")] double? AverageRating,
    [property: JsonPropertyName("ratingCount")] int? RatingCount,
    [property: JsonPropertyName("subscriberCount")] int? SubscriberCount,
    [property: JsonPropertyName("currentVersionNumber")] string CurrentVersionNumber,
    [property: JsonPropertyName("currentVersionSize")] long? CurrentVersionSize,
    [property: JsonPropertyName("previews")] IReadOnlyList<Preview> Previews,
    [property: JsonPropertyName("meta")] object Meta,
    [property: JsonPropertyName("createdAt")] DateTime? CreatedAt,
    [property: JsonPropertyName("updatedAt")] DateTime? UpdatedAt,
    [property: JsonPropertyName("currentVersionId")] int? CurrentVersionId,
    [property: JsonPropertyName("dependencyTree")] DependencyTree DependencyTree,
    [property: JsonPropertyName("tags")] IReadOnlyList<Tag> Tags,
    [property: JsonPropertyName("author")] Author Author
);