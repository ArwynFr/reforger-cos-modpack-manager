using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using OpenIddict.Client;
using OpenIddict.Client.AspNetCore;

namespace ArwynFr.Reforger.ModpackMgr.Domain;

[Route("/")]
internal class AuthController(
    GoogleAuth googleAuth,
    ILogger<AuthController> logger,
    IOptions<GoogleOptions> options,
    OpenIddictClientService service) : ControllerBase
{
    [HttpGet("google-login")]
    public async Task<IActionResult> Login()
    {
        logger.LogInformation("Google login");
        // On récupère la configuration de votre IDP
        var registration = await service.GetClientRegistrationByIssuerAsync(options.Value.Issuer);

        // OpenIddict va générer l'URL de redirection (avec State, PKCE, etc.)
        // et envoyer un 302 Redirect vers l'IDP.
        return Challenge(
            new AuthenticationProperties { RedirectUri = options.Value.Callback.AbsoluteUri },
            OpenIddictClientAspNetCoreDefaults.AuthenticationScheme);
    }

    [HttpGet("google-callback")]
    public async Task<IActionResult> Callback()
    {
        logger.LogInformation("Google callback");
        // 1. On récupère le résultat de l'IDP
        var result = await HttpContext.AuthenticateAsync(OpenIddictClientAspNetCoreDefaults.AuthenticationScheme);

        if (!result.Succeeded) return BadRequest();

        // 2. On récupère l'Access Token
        googleAuth.Token = result.Properties.GetTokenValue("backchannel_access_token");

        // 3. Stockage et Retour vers Blazor
        // Ici, vous pouvez stocker le token dans un cache (ex: IMemoryCache) lié à l'ID de session 
        // ou le passer via l'URL (moins sécurisé) pour le récupérer dans Blazor.
        return Redirect("/");
    }
}
