// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Linq;

namespace VideoPlatform.AuthenticationService.Quickstart.Account;

/// <summary>
///     LoginViewModel
/// </summary>
public class LoginViewModel : LoginInputModel
{
    /// <summary>
    ///     AllowRememberLogin
    /// </summary>
    public bool AllowRememberLogin { get; set; } = true;

    /// <summary>
    ///     EnableLocalLogin
    /// </summary>
    public bool EnableLocalLogin { get; set; } = true;

    /// <summary>
    ///     ExternalProviders
    /// </summary>
    public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();

    /// <summary>
    ///     VisibleExternalProviders
    /// </summary>
    public IEnumerable<ExternalProvider> VisibleExternalProviders =>
        ExternalProviders.Where(x => !string.IsNullOrWhiteSpace(x.DisplayName));

    /// <summary>
    ///     IsExternalLoginOnly
    /// </summary>
    public bool IsExternalLoginOnly => !EnableLocalLogin && ExternalProviders?.Count() == 1;

    /// <summary>
    ///     ExternalLoginScheme
    /// </summary>
    public string ExternalLoginScheme =>
        IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;
}