
using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public record WorkshopDocumentItem(
    [property: JsonPropertyName("props")] WorkshopItem WorkshopItem
);