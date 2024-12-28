// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Duende.IdentityServer.Models;

namespace VideoPlatform.AuthenticationService.Quickstart.Home;

/// <summary>
///     ErrorViewModel
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    ///     ErrorViewModel
    /// </summary>
    public ErrorViewModel()
    {
    }

    /// <summary>
    ///     ErrorViewModel
    /// </summary>
    /// <param name="error"></param>
    public ErrorViewModel(string error)
    {
        Error = new ErrorMessage { Error = error };
    }

    /// <summary>
    ///     Error
    /// </summary>
    public ErrorMessage Error { get; set; }
}