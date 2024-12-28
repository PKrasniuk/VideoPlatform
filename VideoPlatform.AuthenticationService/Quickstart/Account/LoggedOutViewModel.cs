// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


namespace VideoPlatform.AuthenticationService.Quickstart.Account;

/// <summary>
///     LoggedOutViewModel
/// </summary>
public class LoggedOutViewModel
{
    /// <summary>
    ///     PostLogoutRedirectUri
    /// </summary>
    public string PostLogoutRedirectUri { get; set; }

    /// <summary>
    ///     ClientName
    /// </summary>
    public string ClientName { get; set; }

    /// <summary>
    ///     SignOutIframeUrl
    /// </summary>
    public string SignOutIframeUrl { get; set; }

    /// <summary>
    ///     AutomaticRedirectAfterSignOut
    /// </summary>
    public bool AutomaticRedirectAfterSignOut { get; set; }

    /// <summary>
    ///     LogoutId
    /// </summary>
    public string LogoutId { get; set; }

    /// <summary>
    ///     TriggerExternalSignout
    /// </summary>
    public bool TriggerExternalSignout => ExternalAuthenticationScheme != null;

    /// <summary>
    ///     ExternalAuthenticationScheme
    /// </summary>
    public string ExternalAuthenticationScheme { get; set; }
}