using System.Text.Json;

using HtmlAgilityPack;

using Microsoft.Extensions.Caching.Memory;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public class WorkshopItemRepository(
    IBohemiaInteractiveWorkshop bohemiaInteractiveWorkshop,
    IMemoryCache memoryCache)
{
    public void Clear()
    {
        if (memoryCache is not MemoryCache cache) { return; }
        cache.Clear();
    }

    private Func<ICacheEntry, Task<WorkshopItem?>> FetchWorkshopItem(string id, CancellationToken cancellationToken)
    => async _ => await bohemiaInteractiveWorkshop.GetWorkshopItem(id, cancellationToken) is { IsSuccessStatusCode: true } response ? response.Content : null;

    private async Task<WorkshopItem?> ParseContent(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        HtmlDocument htmlDocument = new();
        htmlDocument.Load(await response.Content.ReadAsStreamAsync(cancellationToken));
        return JsonSerializer.Deserialize<WorkshopDocumentItem>(htmlDocument.GetElementbyId("__NEXT_DATA__").InnerHtml)?.WorkshopItem;
    }

    public async Task<WorkshopItem?> GetItemAsync(string id, CancellationToken cancellationToken)
    => await memoryCache.GetOrCreateAsync(id, FetchWorkshopItem(id, cancellationToken));
}