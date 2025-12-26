using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Duende.IdentityModel;
using Duende.IdentityServer;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.AuthenticationService.Quickstart.Account;

/// <summary>
///     ExternalController
/// </summary>
[SecurityHeaders]
[AllowAnonymous]
public class ExternalController : Controller
{
    private readonly IIdentityServerInteractionService _interaction;
    private readonly ILogger<ExternalController> _logger;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    /// <summary>
    ///     ExternalController
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    /// <param name="interaction"></param>
    /// <param name="logger"></param>
    public ExternalController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IIdentityServerInteractionService interaction,
        ILogger<ExternalController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _interaction = interaction;
        _logger = logger;
    }

    /// <summary>
    ///     initiate roundtrip to external authentication provider
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Challenge(string provider, string returnUrl)
    {
        if (string.IsNullOrEmpty(returnUrl)) returnUrl = "~/";

        // validate returnUrl - either it is a valid OIDC URL or back to a local page
        if (!Url.IsLocalUrl(returnUrl) && !_interaction.IsValidReturnUrl(returnUrl))
            // user might have clicked on a malicious link - should be logged
            throw new Exception("invalid return URL");

        if (AccountOptions.WindowsAuthenticationSchemeName == provider)
            // windows authentication needs special handling
            return await ProcessWindowsLoginAsync(returnUrl);

        // start challenge and roundtrip the return URL and scheme 
        var props = new AuthenticationProperties
        {
            RedirectUri = Url.Action(nameof(Callback)),
            Items =
            {
                { "returnUrl", returnUrl },
                { "scheme", provider }
            }
        };

        return Challenge(props, provider);
    }

    /// <summary>
    ///     Post processing of external authentication
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Callback()
    {
        // read external identity from the temporary cookie
        var result = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
        if (!result.Succeeded) throw new Exception("External authentication error");

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            var externalClaims = result.Principal.Claims.Select(c => $"{c.Type}: {c.Value}");
            _logger.LogDebug("External claims: {@claims}", externalClaims);
        }

        // lookup our user and external provider info
        var (user, provider, providerUserId, claims) = await FindUserFromExternalProviderAsync(result);
        if (user == null)
            // this might be where you might initiate a custom workflow for user registration
            // in this sample we don't show how that would be done, as our sample implementation
            // simply auto-provisions new external user
            user = await AutoProvisionUserAsync(provider, providerUserId, claims);

        // this allows us to collect any additonal claims or properties
        // for the specific prtotocols used and store them in the local auth cookie.
        // this is typically used to store data needed for signout from those protocols.
        var additionalLocalClaims = new List<Claim>();
        var localSignInProps = new AuthenticationProperties();
        ProcessLoginCallbackForOidc(result, additionalLocalClaims, localSignInProps);
        ProcessLoginCallbackForWsFed(result, additionalLocalClaims, localSignInProps);
        ProcessLoginCallbackForSaml2P(result, additionalLocalClaims, localSignInProps);

        // issue authentication cookie for user
        // we must issue the cookie maually, and can't use the SignInManager because
        // it doesn't expose an API to issue additional claims from the login workflow
        var principal = await _signInManager.CreateUserPrincipalAsync(user);
        additionalLocalClaims.AddRange(principal.Claims);
        _ = principal.FindFirst(JwtClaimTypes.Name)?.Value ?? user.Id.ToString();
        //await HttpContext.SignInAsync(user.Id.ToString(), name, provider, localSignInProps, additionalLocalClaims.ToArray());

        // delete temporary cookie used during external authentication
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        // retrieve return URL
        var returnUrl = result.Properties.Items["returnUrl"] ?? "~/";

        // check if external login is in the context of an OIDC request
        await _interaction.GetAuthorizationContextAsync(returnUrl);
        //await _events.RaiseAsync(new UserLoginSuccessEvent(provider, providerUserId, user.Id.ToString(), name, true, context?.ClientId));

        //if (context != null)
        //{
        //    if (await _clientStore.IsPkceClientAsync(context.ClientId))
        //    {
        //        // if the client is PKCE then we assume it's native, so this change in how to
        //        // return the response is for better UX for the end user.
        //        return View("Redirect", new RedirectViewModel { RedirectUrl = returnUrl });
        //    }
        //}

        return Redirect(returnUrl);
    }

    private async Task<IActionResult> ProcessWindowsLoginAsync(string returnUrl)
    {
        // see if windows auth has already been requested and succeeded
        var result = await HttpContext.AuthenticateAsync(AccountOptions.WindowsAuthenticationSchemeName);
        if (result.Principal is WindowsPrincipal wp)
        {
            // we will issue the external cookie and then redirect the
            // user back to the external callback, in essence, treating windows
            // auth the same as any other external authentication mechanism
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action("Callback"),
                Items =
                {
                    { "returnUrl", returnUrl },
                    { "scheme", AccountOptions.WindowsAuthenticationSchemeName }
                }
            };

            var id = new ClaimsIdentity(AccountOptions.WindowsAuthenticationSchemeName);
            if (wp.Identity.Name != null)
            {
                id.AddClaim(new Claim(JwtClaimTypes.Subject, wp.Identity.Name));
                id.AddClaim(new Claim(JwtClaimTypes.Name, wp.Identity.Name));
            }

            // add the groups as claims -- be careful if the number of groups is too large
            if (AccountOptions.IncludeWindowsGroups)
            {
                var wi = wp.Identity as WindowsIdentity;
                if (wi != null && wi.Groups != null)
                {
                    var groups = wi.Groups.Translate(typeof(NTAccount));
                    if (groups != null)
                    {
                        var roles = groups.Select(x => new Claim(JwtClaimTypes.Role, x.Value));
                        id.AddClaims(roles);
                    }
                }
            }

            await HttpContext.SignInAsync(
                IdentityServerConstants.ExternalCookieAuthenticationScheme,
                new ClaimsPrincipal(id),
                props);
            if (props.RedirectUri != null)
                return Redirect(props.RedirectUri);
        }

        // trigger windows auth
        // since windows auth don't support the redirect uri,
        // this URL is re-triggered when we call challenge
        return Challenge(AccountOptions.WindowsAuthenticationSchemeName);
    }

    private async Task<(AppUser user, string provider, string providerUserId, IEnumerable<Claim> claims)>
        FindUserFromExternalProviderAsync(AuthenticateResult result)
    {
        var externalUser = result.Principal;

        // try to determine the unique id of the external user (issued by the provider)
        // the most common claim type for that are the sub claim and the NameIdentifier
        // depending on the external provider, some other claim type might be used
        if (externalUser != null)
        {
            var userIdClaim = externalUser.FindFirst(JwtClaimTypes.Subject) ??
                              externalUser.FindFirst(ClaimTypes.NameIdentifier) ??
                              throw new Exception("Unknown userid");

            // remove the user id claim so we don't include it as an extra claim if/when we provision the user
            var claims = externalUser.Claims.ToList();
            claims.Remove(userIdClaim);

            if (result.Properties != null)
            {
                var provider = result.Properties.Items["scheme"];
                var providerUserId = userIdClaim.Value;

                // find external user
                var user = await _userManager.FindByLoginAsync(provider, providerUserId);

                return (user, provider, providerUserId, claims);
            }
        }

        return (null, null, null, null);
    }

    private async Task<AppUser> AutoProvisionUserAsync(string provider, string providerUserId,
        IEnumerable<Claim> claims)
    {
        // create a list of claims that we want to transfer into our store
        var filtered = new List<Claim>();

        // user's display name
        var enumerable = claims as Claim[] ?? claims.ToArray();
        var name = enumerable.FirstOrDefault(x => x.Type == JwtClaimTypes.Name)?.Value ??
                   enumerable.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        if (name != null)
        {
            filtered.Add(new Claim(JwtClaimTypes.Name, name));
        }
        else
        {
            var first = enumerable.FirstOrDefault(x => x.Type == JwtClaimTypes.GivenName)?.Value ??
                        enumerable.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
            var last = enumerable.FirstOrDefault(x => x.Type == JwtClaimTypes.FamilyName)?.Value ??
                       enumerable.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value;
            if (first != null && last != null)
                filtered.Add(new Claim(JwtClaimTypes.Name, first + " " + last));
            else if (first != null)
                filtered.Add(new Claim(JwtClaimTypes.Name, first));
            else if (last != null) filtered.Add(new Claim(JwtClaimTypes.Name, last));
        }

        // email
        var email = enumerable.FirstOrDefault(x => x.Type == JwtClaimTypes.Email)?.Value ??
                    enumerable.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (email != null) filtered.Add(new Claim(JwtClaimTypes.Email, email));

        var user = new AppUser
        {
            UserName = Guid.NewGuid().ToString()
        };
        var identityResult = await _userManager.CreateAsync(user);
        if (!identityResult.Succeeded) throw new Exception(identityResult.Errors.First().Description);

        if (filtered.Any())
        {
            identityResult = await _userManager.AddClaimsAsync(user, filtered);
            if (!identityResult.Succeeded) throw new Exception(identityResult.Errors.First().Description);
        }

        identityResult = await _userManager.AddLoginAsync(user, new UserLoginInfo(provider, providerUserId, provider));
        if (!identityResult.Succeeded) throw new Exception(identityResult.Errors.First().Description);

        return user;
    }


    private void ProcessLoginCallbackForOidc(AuthenticateResult externalResult, List<Claim> localClaims,
        AuthenticationProperties localSignInProps)
    {
        // if the external system sent a session id claim, copy it over
        // so we can use it for single sign-out
        if (externalResult.Principal != null)
        {
            var sid = externalResult.Principal.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
            if (sid != null) localClaims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));
        }

        // if the external provider issued an id_token, we'll keep it for signout
        if (externalResult.Properties != null)
        {
            var idToken = externalResult.Properties.GetTokenValue("id_token");
            if (idToken != null)
                localSignInProps.StoreTokens(new[] { new AuthenticationToken { Name = "id_token", Value = idToken } });
        }
    }

    private void ProcessLoginCallbackForWsFed(AuthenticateResult externalResult, List<Claim> localClaims,
        AuthenticationProperties localSignInProps)
    {
    }

    private static void ProcessLoginCallbackForSaml2P(AuthenticateResult externalResult, List<Claim> localClaims,
        AuthenticationProperties localSignInProps)
    {
    }
}