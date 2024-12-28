using System;
using System.Collections.Generic;
using Duende.IdentityServer.Models;
using Microsoft.Extensions.Configuration;

namespace VideoPlatform.AuthenticationService.Infrastructure.Configurations;

/// <summary>
///     Clients Configuration
/// </summary>
public class ClientsConfiguration
{
    /// <summary>
    ///     Clients Configuration Constructor
    /// </summary>
    /// <param name="configuration"></param>
    public ClientsConfiguration(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    private IConfiguration Configuration { get; }

    /// <summary>
    ///     GetIdentityResources
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };
    }

    /// <summary>
    ///     GetApiResources
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ApiResource> GetApiResources()
    {
        yield return new ApiResource
        {
            Name = Configuration["Security:ApiResources:Name"] ?? string.Empty,
            DisplayName = Configuration["Security:ApiResources:DisplayName"]
            //Scopes = new[]
            //{
            //    //new Scope("readAccess", "Access read operations"),
            //    //new Scope("writeAccess", "Access write operations")
            //}
        };
    }

    /// <summary>
    ///     GetClients
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Client> GetClients()
    {
        return new[]
        {
            new Client
            {
                AllowAccessTokensViaBrowser = true,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes = new[] { "readAccess", "writeAccess" },
                ClientId = Configuration["Security:ApiClient:ClientId"] ?? string.Empty,
                ClientName = Configuration["Security:ApiClient:ClientName"],
                ClientSecrets = new[] { new Secret(Configuration["Security:ApiClient:ClientSecret"].Sha256()) },
                RedirectUris = new[] { Configuration["Security:ApiClient:RedirectUrl"] }
            }
        };
    }
}