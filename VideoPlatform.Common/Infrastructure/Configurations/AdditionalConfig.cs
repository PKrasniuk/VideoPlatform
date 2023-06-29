using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;

namespace VideoPlatform.Common.Infrastructure.Configurations;

public class AdditionalConfig
{
    public static void ConfigureHsts(HstsOptions options)
    {
        options.Preload = true;
        options.IncludeSubDomains = true;
        options.MaxAge = TimeSpan.FromDays(60);
    }

    public static void ConfigureHttpsRedirection(HttpsRedirectionOptions options)
    {
        options.RedirectStatusCode = StatusCodes.Status301MovedPermanently;
    }
}