
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record SearchResult(
    [property: JsonPropertyName("pageProps")] PagePropsSearch PagePropsSearch,
    [property: JsonPropertyName("__N_SSP")] bool? NSSP
);