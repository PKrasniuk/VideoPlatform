// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


namespace VideoPlatform.AuthenticationService.Quickstart.Account;

/// <summary>
///     LogoutViewModel
/// </summary>
public class LogoutViewModel : LogoutInputModel
{
    /// <summary>
    ///     ShowLogoutPrompt
    /// </summary>
    public bool ShowLogoutPrompt { get; set; } = true;
}