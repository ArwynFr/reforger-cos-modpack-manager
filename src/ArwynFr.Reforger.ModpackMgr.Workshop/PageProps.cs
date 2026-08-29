
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record PageProps(
    [property: JsonPropertyName("pathId")] string PathId,
    [property: JsonPropertyName("asset")] Asset Asset,
    [property: JsonPropertyName("assetVersionDetail")] AssetVersionDetail AssetVersionDetail,
    [property: JsonPropertyName("getAssetDownloadTotal")] GetAssetDownloadTotal GetAssetDownloadTotal
);