using ArwynFr.Reforger.ModpackMgr.Workshop;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArwynFr.Reforger.ModpackMgr.Domain;

public record Mod(string Id)
{
    public int Order { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public required bool Enabled { get; set; }
    public ICollection<Mod> Dependencies { get; init; } = [];
    public ICollection<Mod> Dependants { get; init; } = [];

    public string Name => WorkshopInformation?.Name ?? string.Empty;
    public string EffectiveVersion => Version is not "" ? Version : WorkshopInformation?.Version ?? string.Empty;
    public System.Version? GameVersion => WorkshopInformation?.GameVersion;
    public long? Size => WorkshopInformation?.Size;

    private WorkshopInformation? WorkshopInformation { get; set; }

    public async Task FetchWorkshopInformation(DbSet<Mod> mods, WorkshopItemRepository workshopItemRepository, CancellationToken cancellationToken)
    {
        WorkshopInformation = await workshopItemRepository.GetItemAsync(Id, cancellationToken) switch
        {
            WorkshopItem item => WorkshopInformation.Factory(item),
            _ => null
        };

        Dependencies.Clear();
        foreach (var id in WorkshopInformation?.Dependencies ?? [])
        {
            if (await mods.FindAsync([id], cancellationToken) is Mod mod)
            {
                Dependencies.Add(mod);
            }
            else
            {
                var entity = mods.Add(new(id) { Enabled = true, Dependants = { this }, Order = -1 }).Entity;
                await entity.FetchWorkshopInformation(mods, workshopItemRepository, cancellationToken);
            }
        }
    }

    private class EntityConfiguration : IEntityTypeConfiguration<Mod>
    {
        public void Configure(EntityTypeBuilder<Mod> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.OwnsOne(_ => _.WorkshopInformation)
                .Property(_ => _.GameVersion).HasConversion(
                    value => value!.ToString(2),
                    value => System.Version.Parse(value));
            builder.HasMany(_ => _.Dependencies).WithMany(_ => _.Dependants);
        }
    }
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