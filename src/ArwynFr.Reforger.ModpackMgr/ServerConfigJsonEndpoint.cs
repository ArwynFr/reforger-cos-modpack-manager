using System.Runtime.CompilerServices;

using ArwynFr.Reforger.ModpackMgr.Database;
using ArwynFr.Reforger.ModpackMgr.Domain;
using ArwynFr.Reforger.ModpackMgr.ServerConfig;
using ArwynFr.Reforger.ModpackMgr.Workshop;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArwynFr.Reforger.ModpackMgr;

internal static class ServerConfigJsonEndpoint
{
    internal static async Task<IResult> Execute(
        [FromServices] ModsDbContext modsDbContext,
        [FromServices] WorkshopItemRepository workshopItemRepository,
        CancellationToken cancellationToken)
    {
        return Results.Ok(await GetServerConfiguration(modsDbContext, workshopItemRepository, cancellationToken));
    }

    public static Task<ServerConfigurationModEntry[]> GetServerConfiguration(
        ModsDbContext modsDbContext,
        WorkshopItemRepository workshopItemRepository,
        CancellationToken cancellationToken)
    => modsDbContext.Mods.AsNoTracking()
        .Where(_ => _.Enabled)
        .OrderBy(_ => _.Order)
        .Select(_ => new ServerConfigurationModEntry(_.Id, _.Name, _.EffectiveVersion))
        .ToArrayAsync(cancellationToken);

    private static async IAsyncEnumerable<ServerConfigurationModEntry> GetDependencies(
        ModsDbContext modsDbContext,
        WorkshopItemRepository workshopItemRepository,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var mod in modsDbContext.Mods.Where(_ => _.Enabled).ToAsyncEnumerable())
        {
            await foreach (var id in GetDependencies(mod.Id, modsDbContext, workshopItemRepository, cancellationToken))
            {
                yield return await Convert(id, modsDbContext, workshopItemRepository, cancellationToken);
            }
        }
    }

    private static async Task<ServerConfigurationModEntry> Convert(
        string id,
        ModsDbContext modsDbContext,
        WorkshopItemRepository workshopItemRepository,
        CancellationToken cancellationToken)
    => await modsDbContext.Mods.FindAsync(id, cancellationToken) switch
    {
        Mod mod => new(mod.Id, mod.Name, mod.EffectiveVersion),

        //     { WorkshopInformation: WorkshopInformation info } mod => Convert(id, mod.Version, info),
        //     Mod mod when await workshopItemRepository.GetItemAsync(id, cancellationToken) is WorkshopItem item
        //   => Convert(id, mod.Version, WorkshopInformation.Factory(item)),
        //     _ when await workshopItemRepository.GetItemAsync(id, cancellationToken) is WorkshopItem item
        //   => Convert(id, string.Empty, WorkshopInformation.Factory(item)),
        _ => throw new InvalidOperationException($"Convert {id}")
    };

    private static ServerConfigurationModEntry Convert(string id, string version, WorkshopInformation workshopInformation)
    => new(id, workshopInformation.Name, version switch { "" => workshopInformation.Version, _ => version });

    private static async IAsyncEnumerable<string> GetDependencies(
      string id,
      ModsDbContext modsDbContext,
      WorkshopItemRepository workshopItemRepository,
      [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var result = await modsDbContext.Mods.FindAsync(id) switch
        {
            Mod mod => mod.Dependencies.Select(_ => _.Id),
            // { WorkshopInformation: WorkshopInformation info } => info.Dependencies,
            // _ when await workshopItemRepository.GetItemAsync(id, cancellationToken) is WorkshopItem item
            //   => WorkshopInformation.Factory(item).Dependencies,
            _ => throw new InvalidOperationException($"Dependencies {id}")
        };
        foreach (var item in result) { yield return item; }
        yield return id;
    }

}