// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


namespace VideoPlatform.AuthenticationService.Quickstart.Consent;

/// <summary>
///     ScopeViewModel
/// </summary>
public class ScopeViewModel
{
    /// <summary>
    ///     Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     DisplayName
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    ///     Description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Emphasize
    /// </summary>
    public bool Emphasize { get; set; }

    /// <summary>
    ///     Required
    /// </summary>
    public bool Required { get; set; }

    /// <summary>
    ///     Checked
    /// </summary>
    public bool Checked { get; set; }
}