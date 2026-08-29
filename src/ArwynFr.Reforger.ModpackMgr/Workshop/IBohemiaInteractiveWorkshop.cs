using Refit;

namespace ArwynFr.Reforger.ModpackMgr.Workshop;

public interface IBohemiaInteractiveWorkshop
{
    [Get("/workshop/{id}.json")]
    public Task<ApiResponse<WorkshopItem?>> GetWorkshopItem([Query] string id, CancellationToken cancellationToken);

    [Get("/workshop/{id}")]
    public Task<HttpResponseMessage> GetWorkshopItemFromHtml([Query] string id, CancellationToken cancellationToken);

    [Get("/workshop.json")]
    public Task<SearchResult?> SearchWorkshopItems([Query] string search, CancellationToken cancellationToken);

}
