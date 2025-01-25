// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Text;
using Duende.IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;

namespace VideoPlatform.AuthenticationService.Quickstart.Diagnostics;

/// <summary>
///     DiagnosticsViewModel
/// </summary>
public class DiagnosticsViewModel
{
    /// <summary>
    ///     DiagnosticsViewModel
    /// </summary>
    /// <param name="result"></param>
    public DiagnosticsViewModel(AuthenticateResult result)
    {
        AuthenticateResult = result;

        if (result.Properties != null && result.Properties.Items.TryGetValue("client_list", out var encoded))
            if (encoded != null)
            {
                var bytes = Base64Url.Decode(encoded);
                var value = Encoding.UTF8.GetString(bytes);

                Clients = JsonConvert.DeserializeObject<string[]>(value);
            }
    }

    /// <summary>
    ///     AuthenticateResult
    /// </summary>
    public AuthenticateResult AuthenticateResult { get; }

    /// <summary>
    ///     Clients
    /// </summary>
    public IEnumerable<string> Clients { get; } = new List<string>();
}