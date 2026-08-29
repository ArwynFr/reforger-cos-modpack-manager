
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record WorkshopItem(
    [property: JsonPropertyName("pageProps")] PageProps PageProps,
    [property: JsonPropertyName("__N_SSP")] bool? NSSP
);