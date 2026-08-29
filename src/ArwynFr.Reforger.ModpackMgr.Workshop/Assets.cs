
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record Assets(
    [property: JsonPropertyName("count")] int? Count,
    [property: JsonPropertyName("rows")] IReadOnlyList<Row> Rows
);