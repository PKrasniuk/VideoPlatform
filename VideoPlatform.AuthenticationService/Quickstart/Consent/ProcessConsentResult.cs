// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


namespace VideoPlatform.AuthenticationService.Quickstart.Consent;

/// <summary>
///     ResourceScopes
/// </summary>
public class ProcessConsentResult
{
    /// <summary>
    ///     IsRedirect
    /// </summary>
    public bool IsRedirect => RedirectUri != null;

    /// <summary>
    ///     RedirectUri
    /// </summary>
    public string RedirectUri { get; set; }

    /// <summary>
    ///     ClientId
    /// </summary>
    public string ClientId { get; set; }

    /// <summary>
    ///     ShowView
    /// </summary>
    public bool ShowView => ViewModel != null;

    /// <summary>
    ///     ViewModel
    /// </summary>
    public ConsentViewModel ViewModel { get; set; }

    /// <summary>
    ///     HasValidationError
    /// </summary>
    public bool HasValidationError => ValidationError != null;

    /// <summary>
    ///     ValidationError
    /// </summary>
    public string ValidationError { get; set; }
}