using System;
using System.Collections.Generic;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace VideoPlatform.AuthenticationService.Infrastructure.Configurations
{
    /// <summary>
    /// Clients Configuration
    /// </summary>
    public class ClientsConfiguration
    {
        private readonly IConfiguration _appConfiguration;

        /// <summary>
        /// Clients Configuration Constructor
        /// </summary>
        /// <param name="appConfiguration"></param>
        public ClientsConfiguration(IConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration ?? throw new ArgumentNullException(nameof(appConfiguration));
        }

        /// <summary>
        /// GetIdentityResources
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
        /// GetApiResources
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApiResource> GetApiResources()
        {
            yield return new ApiResource
            {
                Name = _appConfiguration["Security:ApiResources:Name"],
                DisplayName = _appConfiguration["Security:ApiResources:DisplayName"],
                Scopes = new[]
                {
                    new Scope("readAccess", "Access read operations"),
                    new Scope("writeAccess", "Access write operations")
                }
            };
        }

        /// <summary>
        /// GetClients
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
                    ClientId = _appConfiguration["Security:ApiClient:ClientId"],
                    ClientName = _appConfiguration["Security:ApiClient:ClientName"],
                    ClientSecrets = new[] { new Secret(_appConfiguration["Security:ApiClient:ClientSecret"].Sha256()) },
                    RedirectUris = new [] { _appConfiguration["Security:ApiClient:RedirectUrl"] }
                }
            };
        }
    }
}
