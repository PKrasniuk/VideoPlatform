using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.Api.Infrastructure.Configurations;

namespace VideoPlatform.Api.Infrastructure.Extensions
{
    internal static partial class ConfigurationExtension
    {
        internal static void AddResponseConfiguration(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = new[]
                {
                    // Default
                    MimeTypeNames.TextPlain,
                    MimeTypeNames.TextCss,
                    MimeTypeNames.ApplicationJavascript,
                    MimeTypeNames.TextHtml,
                    "application/xml",
                    MimeTypeNames.TextXml,
                    MimeTypeNames.ApplicationJson,
                    "text/json",
                    // Custom
                    MimeTypeNames.ImageSvgXml
                };
                options.EnableForHttps = true;
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
        }
    }
}