using ArwynFr.Reforger.ModpackMgr.Workshop;

namespace ArwynFr.Reforger.ModpackMgr.Domain;


public class WorkshopAdapter(IBohemiaInteractiveWorkshop bohemiaInteractiveWorkshop)
{

    public async Task<IEnumerable<(string id, string name)>> Search(string needle, CancellationToken cancellationToken)
    => await bohemiaInteractiveWorkshop.SearchWorkshopItems(needle, cancellationToken) switch
    {
        SearchResult response => response.PagePropsSearch.Assets.Rows.Select(_ => (_.Id, _.Name)),
        _ => []
    };
}