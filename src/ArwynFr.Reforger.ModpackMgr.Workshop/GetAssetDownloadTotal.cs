
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record GetAssetDownloadTotal(
    [property: JsonPropertyName("total")] int? Total
);