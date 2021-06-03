using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;

namespace VideoPlatform.Common.Infrastructure.Extensions
{
    public static class ControllersExtension
    {
        public static IServiceCollection AddControllersConfiguration(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.EnableEndpointRouting = true;
            }).AddFluentValidation().AddNewtonsoftJson(options =>
                options.SerializerSettings.Converters.Add(new StringEnumConverter()));
            services.AddRouting(options => options.LowercaseUrls = true);

            return services;
        }
    }
}