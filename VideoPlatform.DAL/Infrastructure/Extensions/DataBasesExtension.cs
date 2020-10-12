﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.DAL.Infrastructure.Configurations;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Infrastructure.Extensions
{
    public static partial class ConfigurationExtension
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfigurationRoot configuration)
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

            services.Configure<MetaDataAccessConfiguration>(configuration.GetSection(ConfigurationConstants.MetaDataAccessName));

            services.AddTransient(serviceProvider =>
                new MetaContext(serviceProvider.GetService<IOptions<MetaDataAccessConfiguration>>()));

            services.Configure<CosmosDataAccessConfiguration>(configuration.GetSection(ConfigurationConstants.CosmosDataAccessName));

            services.AddTransient(serviceProvider =>
                new CosmosContext(serviceProvider.GetService<IOptions<CosmosDataAccessConfiguration>>()));

            return services;
        }
    }
}