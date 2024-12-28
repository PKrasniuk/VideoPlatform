// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;

namespace VideoPlatform.AuthenticationService.Quickstart.Grants;

/// <summary>
///     GrantsViewModel
/// </summary>
public class GrantsViewModel
{
    public IEnumerable<GrantViewModel> Grants { get; set; }
}

/// <summary>
///     GrantsViewModel
/// </summary>
public class GrantViewModel
{
    /// <summary>
    ///     ClientId
    /// </summary>
    public string ClientId { get; set; }

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
    ///     Created
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    ///     Expires
    /// </summary>
    public DateTime? Expires { get; set; }

    /// <summary>
    ///     IdentityGrantNames
    /// </summary>
    public IEnumerable<string> IdentityGrantNames { get; set; }

    /// <summary>
    ///     ApiGrantNames
    /// </summary>
    public IEnumerable<string> ApiGrantNames { get; set; }
}