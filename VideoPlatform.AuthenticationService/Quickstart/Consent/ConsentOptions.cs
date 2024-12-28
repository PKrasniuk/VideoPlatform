// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


namespace VideoPlatform.AuthenticationService.Quickstart.Consent;

/// <summary>
///     ConsentOptions
/// </summary>
public static class ConsentOptions
{
    /// <summary>
    ///     EnableOfflineAccess
    /// </summary>
    public static readonly bool EnableOfflineAccess = true;

    /// <summary>
    ///     OfflineAccessDisplayName
    /// </summary>
    public static readonly string OfflineAccessDisplayName = "Offline Access";

    /// <summary>
    ///     OfflineAccessDescription
    /// </summary>
    public static readonly string OfflineAccessDescription =
        "Access to your applications and resources, even when you are offline";

    /// <summary>
    ///     MustChooseOneErrorMessage
    /// </summary>
    public static readonly string MustChooseOneErrorMessage = "You must pick at least one permission";

    /// <summary>
    ///     InvalidSelectionErrorMessage
    /// </summary>
    public static readonly string InvalidSelectionErrorMessage = "Invalid selection";
}