using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.AuthenticationService.Infrastructure.Configurations;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.DAL;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.AuthenticationService.Infrastructure.Extensions
{
    internal static class ConfigurationExtension
    {
        public static IServiceCollection AddIdentityServerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer();

            services.AddDbContextPool<VideoPlatformContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString(ConfigurationConstants.ConnectionStringName));
                optionsBuilder.UseInternalServiceProvider(serviceProvider);
                optionsBuilder.UseLazyLoadingProxies(false);
            });

            services.AddIdentity<AppUser, AppRole>(options => { options.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<VideoPlatformContext>()
                .AddDefaultTokenProviders();

            var clientsConfiguration = new ClientsConfiguration(configuration);

            services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(ClientsConfiguration.GetIdentityResources())
                .AddInMemoryApiResources(clientsConfiguration.GetApiResources())
                .AddInMemoryClients(clientsConfiguration.GetClients())
                .AddAspNetIdentity<AppUser>();

            return services;
        }
    }
}