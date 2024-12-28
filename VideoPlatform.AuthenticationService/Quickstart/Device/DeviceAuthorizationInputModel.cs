// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using VideoPlatform.AuthenticationService.Quickstart.Consent;

namespace VideoPlatform.AuthenticationService.Quickstart.Device;

/// <summary>
///     DeviceAuthorizationInputModel
/// </summary>
public class DeviceAuthorizationInputModel : ConsentInputModel
{
    /// <summary>
    ///     UserCode
    /// </summary>
    public string UserCode { get; set; }
}