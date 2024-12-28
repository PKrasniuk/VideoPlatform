using System.Threading.Tasks;
using Duende.IdentityServer.Stores;

namespace VideoPlatform.AuthenticationService.Quickstart;

/// <summary>
///     Extensions
/// </summary>
public static class Extensions
{
    /// <summary>
    ///     Determines whether the client is configured to use PKCE.
    /// </summary>
    /// <param name="store">The store.</param>
    /// <param name="client_id">The client identifier.</param>
    /// <returns></returns>
    public static async Task<bool> IsPkceClientAsync(this IClientStore store, string client_id)
    {
        if (!string.IsNullOrWhiteSpace(client_id))
        {
            var client = await store.FindEnabledClientByIdAsync(client_id);
            return client?.RequirePkce == true;
        }

        return false;
    }
}