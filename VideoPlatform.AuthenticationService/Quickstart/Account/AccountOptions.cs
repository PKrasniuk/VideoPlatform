// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using Microsoft.AspNetCore.Server.IISIntegration;

namespace VideoPlatform.AuthenticationService.Quickstart.Account;

/// <summary>
///     AccountOptions
/// </summary>
public static class AccountOptions
{
    /// <summary>
    ///     AllowLocalLogin
    /// </summary>
    public static readonly bool AllowLocalLogin = true;

    /// <summary>
    ///     AllowRememberLogin
    /// </summary>
    public static readonly bool AllowRememberLogin = true;

    /// <summary>
    ///     RememberMeLoginDuration
    /// </summary>
    public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

    /// <summary>
    ///     ShowLogoutPrompt
    /// </summary>
    public static readonly bool ShowLogoutPrompt = true;

    /// <summary>
    ///     AutomaticRedirectAfterSignOut
    /// </summary>
    public static readonly bool AutomaticRedirectAfterSignOut = false;

    // specify the Windows authentication scheme being used
    /// <summary>
    ///     WindowsAuthenticationSchemeName
    /// </summary>
    public static readonly string WindowsAuthenticationSchemeName = IISDefaults.AuthenticationScheme;

    // if user uses windows auth, should we load the groups from windows
    /// <summary>
    ///     IncludeWindowsGroups
    /// </summary>
    public static readonly bool IncludeWindowsGroups = false;

    /// <summary>
    ///     InvalidCredentialsErrorMessage
    /// </summary>
    public static readonly string InvalidCredentialsErrorMessage = "Invalid username or password";
}