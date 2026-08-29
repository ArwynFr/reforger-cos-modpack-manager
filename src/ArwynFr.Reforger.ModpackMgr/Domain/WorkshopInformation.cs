using ArwynFr.Reforger.ModpackMgr.Workshop;

namespace ArwynFr.Reforger.ModpackMgr.Domain;

public record WorkshopInformation(string Id)
{
    public required string Name { get; init; }
    public required string Version { get; init; }
    public required System.Version? GameVersion { get; init; }
    public required long? Size { get; init; }
    public required string[] Dependencies { get; init; }

    public static WorkshopInformation Factory(WorkshopItem item) => new(item.PageProps.Asset.Id)
    {
        Name = item.PageProps.Asset.Name,
        Version = item.PageProps.Asset.CurrentVersionNumber,
        GameVersion = System.Version.TryParse(item.PageProps.Asset.GameVersion, out var version) ? version : null,
        Dependencies = [.. item.PageProps.Asset.Dependencies.Select(_ => _.Asset.Id)],
        Size = item.PageProps.Asset.CurrentVersionSize
    };
}

// public class WorkshopItemRepository(
//     IBohemiaInteractiveWorkshop bohemiaInteractiveWorkshop,
//     IMemoryCache memoryCache)
// {
//     private Func<ICacheEntry, Task<WorkshopItem?>> FetchWorkshopItem(string id, CancellationToken cancellationToken)
//     => async _ => await bohemiaInteractiveWorkshop.GetWorkshopItem(id, cancellationToken);

//     public async Task<WorkshopItem?> GetItemAsync(string id, CancellationToken cancellationToken)
//     => await memoryCache.GetOrCreateAsync(id, FetchWorkshopItem(id, cancellationToken));
// }