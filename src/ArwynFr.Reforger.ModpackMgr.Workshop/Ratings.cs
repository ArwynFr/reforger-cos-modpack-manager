
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record Ratings(
    [property: JsonPropertyName("likes")] int? Likes,
    [property: JsonPropertyName("dislikes")] int? Dislikes,
    [property: JsonPropertyName("rating")] decimal? Rating
);