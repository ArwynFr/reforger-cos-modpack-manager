
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record Author(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("roles")] IReadOnlyList<string> Roles,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("personalBlocked")] bool? PersonalBlocked
);