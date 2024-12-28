// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;

namespace VideoPlatform.AuthenticationService.Quickstart.Consent;

/// <summary>
///     ConsentInputModel
/// </summary>
public class ConsentInputModel
{
    /// <summary>
    ///     Button
    /// </summary>
    public string Button { get; set; }

    /// <summary>
    ///     ScopesConsented
    /// </summary>
    public IEnumerable<string> ScopesConsented { get; set; }

    /// <summary>
    ///     RememberConsent
    /// </summary>
    public bool RememberConsent { get; set; }

    /// <summary>
    ///     ReturnUrl
    /// </summary>
    public string ReturnUrl { get; set; }
}