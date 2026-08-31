using ArwynFr.Reforger.ModpackMgr;
using ArwynFr.Reforger.ModpackMgr.Components;
using ArwynFr.Reforger.ModpackMgr.Database;
using ArwynFr.Reforger.ModpackMgr.Domain;
using ArwynFr.Reforger.ModpackMgr.Workshop;

using Refit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<ModsDbContext>();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddSingleton<WorkshopAdapter>();
builder.Services.AddHttpClient();
builder.Services.AddRefitClient<IBohemiaInteractiveWorkshop>().ConfigureHttpClient(client =>
{
    client.BaseAddress = new(builder.Configuration.GetValue<string>("ReforgerWorkshop:BaseAddress"));
});
builder.Host.UseSystemd();
builder.Services.AddSingleton<WorkshopItemRepository>();
builder.Services.AddMemoryCache();
GoogleAdapter.Register(builder);
builder.Services.AddControllers();
CustomControllerFeatureProvider.Register(builder);
builder.AddSecurity();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();

app.MapGet("/server.json", ServerConfigJsonEndpoint.Execute);

await app.RunAsync();