// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using VideoPlatform.AuthenticationService.Quickstart.Consent;

namespace VideoPlatform.AuthenticationService.Quickstart.Device;

/// <summary>
///     DeviceAuthorizationViewModel
/// </summary>
public class DeviceAuthorizationViewModel : ConsentViewModel
{
    /// <summary>
    ///     UserCode
    /// </summary>
    public string UserCode { get; set; }

    /// <summary>
    ///     ConfirmUserCode
    /// </summary>
    public bool ConfirmUserCode { get; set; }
}