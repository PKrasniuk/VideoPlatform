// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

namespace VideoPlatform.AuthenticationService.Quickstart.Account;

/// <summary>
///     LoginInputModel
/// </summary>
public class LoginInputModel
{
    /// <summary>
    ///     Username
    /// </summary>
    [Required]
    public string Username { get; set; }

    /// <summary>
    ///     Password
    /// </summary>
    [Required]
    public string Password { get; set; }

    /// <summary>
    ///     RememberLogin
    /// </summary>
    public bool RememberLogin { get; set; }

    /// <summary>
    ///     ReturnUrl
    /// </summary>
    public string ReturnUrl { get; set; }
}