using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

using OpenIddict.Client;

using Refit;

namespace ArwynFr.Reforger.ModpackMgr.Domain;

internal class GoogleAdapter(
    ILogger<GoogleAdapter> logger,
    NavigationManager navigationManager,
    IGoogleSheetsApi googleSheetsApi,
    IOptions<GoogleOptions> options)
{
    public async IAsyncEnumerable<GoogleModInformation> FetchMods([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var dto = await GetFromGoogleSheets(cancellationToken);
        foreach (var value in dto?.Values?.Skip(1) ?? [])
        {
            if (Convert(value) is GoogleModInformation mod)
            {
                yield return mod;
            }
        }
    }

    public record GoogleModInformation(string Id, string Category, string Version);

    private async Task<GetValuesResponse?> GetFromGoogleSheets(CancellationToken cancellationToken)
    {
        try
        {

            return await googleSheetsApi.GetValuesAsync(options.Value.Spreadsheet, options.Value.Tab, options.Value.Range, cancellationToken);
        }
        catch (InvalidOperationException)
        {
            logger.LogInformation("Redirect to authentication");
            navigationManager.NavigateTo("/google-login", forceLoad: true);
            return null;
        }
    }

    private static GoogleModInformation? Convert(string[] value) => value switch
    {
        { Length: 3 } => new(value[0], value[1], value[2]),
        { Length: 2 } => new(value[0], value[1], string.Empty),
        _ => null
    };

    public static void Register(WebApplicationBuilder webApplicationBuilder)
    {
        GoogleOptions.Register(webApplicationBuilder);
        IGoogleSheetsApi.Register(webApplicationBuilder);
        webApplicationBuilder.Services.AddScoped<GoogleAdapter>();
    }
}

internal record GoogleOptions
{
    private const string ConfigurationSection = "Google";
    public string? ClientId { get; init; }
    public string? ClientSecret { get; init; }
    public Uri? Callback { get; init; }
    public Uri? Issuer { get; init; }
    public string? Spreadsheet { get; init; }
    public string? Tab { get; init; }
    public string? Range { get; init; }

    public static void Register(WebApplicationBuilder webApplicationBuilder)
    => webApplicationBuilder.Services.AddOptions<GoogleOptions>().BindConfiguration(ConfigurationSection);

    public static GoogleOptions? Get(WebApplicationBuilder webApplicationBuilder)
    => webApplicationBuilder.Configuration.GetSection(ConfigurationSection).Get<GoogleOptions>();
}

internal interface IGoogleSheetsApi
{
    private const string ApiBaseAddress = "https://sheets.googleapis.com/v4/spreadsheets";

    [Get("/{spreadsheet_id}/values/{tab_name}!{range}")]
    Task<GetValuesResponse> GetValuesAsync(string spreadsheet_id, string tab_name, string range, CancellationToken cancellationToken);

    public static void Register(WebApplicationBuilder webApplicationBuilder)
    {
        GoogleAuth.Register(webApplicationBuilder);
        webApplicationBuilder.Services
            .AddRefitClient<IGoogleSheetsApi>()
            .ConfigureHttpClient(client => client.BaseAddress = new(ApiBaseAddress))
            .AddHttpMessageHandler<GoogleAuth>();
    }
}

internal record GetValuesResponse
{
    public string? Range { get; set; }
    public string[][]? Values { get; set; }
}

internal class GoogleAuth(
    ILogger<GoogleAuth> logger,
    IMemoryCache memoryCache) : DelegatingHandler
{
    public string? Token
    {
        get => memoryCache.TryGetValue("token", out string? token) ? token : null;
        set => memoryCache.Set("token", value);
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (Token is not { Length: > 0 } token)
        {
            logger.LogInformation("Token is empty");
            throw new InvalidOperationException();
        }
        request.Headers.Authorization = new("Bearer", token);
        return base.SendAsync(request, cancellationToken);
    }

    public static void Register(WebApplicationBuilder webApplicationBuilder)
    {
    }

    private static void UseSystemNetHttp(OpenIddictClientSystemNetHttpBuilder builder)
    {
        builder.ConfigureHttpClient(ConfigureHttpClient);
    }

    private static void ConfigureHttpClient(HttpClient client)
    {
        client.Timeout = TimeSpan.FromMinutes(3);
    }

    private static void UseAspNetCore(OpenIddictClientAspNetCoreBuilder builder)
    => builder.EnableRedirectionEndpointPassthrough().DisableTransportSecurityRequirement();
}
