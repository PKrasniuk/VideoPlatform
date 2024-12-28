// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;

namespace VideoPlatform.AuthenticationService.Quickstart.Consent;

/// <summary>
///     ConsentViewModel
/// </summary>
public class ConsentViewModel : ConsentInputModel
{
    /// <summary>
    ///     ClientName
    /// </summary>
    public string ClientName { get; set; }

    /// <summary>
    ///     ClientUrl
    /// </summary>
    public string ClientUrl { get; set; }

    /// <summary>
    ///     ClientLogoUrl
    /// </summary>
    public string ClientLogoUrl { get; set; }

    /// <summary>
    ///     AllowRememberConsent
    /// </summary>
    public bool AllowRememberConsent { get; set; }

    /// <summary>
    ///     IdentityScopes
    /// </summary>
    public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }

    /// <summary>
    ///     ResourceScopes
    /// </summary>
    public IEnumerable<ScopeViewModel> ResourceScopes { get; set; }
}