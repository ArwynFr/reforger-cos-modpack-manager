using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace ArwynFr.Reforger.ModpackMgr.Domain;

class CustomControllerFeatureProvider : ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo typeInfo) => typeInfo is { IsAbstract: false } && typeof(ControllerBase).IsAssignableFrom(typeInfo);

    public static void Register(WebApplicationBuilder webApplicationBuilder)
    => webApplicationBuilder.Services.AddMvc().ConfigureApplicationPartManager(ConfigureApplicationPartManager);

    private static void ConfigureApplicationPartManager(ApplicationPartManager manager)
    => manager.FeatureProviders.Add(new CustomControllerFeatureProvider());
}