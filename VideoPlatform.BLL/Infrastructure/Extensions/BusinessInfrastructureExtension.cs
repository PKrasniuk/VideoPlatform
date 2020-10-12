using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.AIL.Infrastructure.Extensions;
using VideoPlatform.CacheService.Infrastructure.Extensions;
using VideoPlatform.CQRS.Infrastructure.Extensions;
using VideoPlatform.DAL.Infrastructure.Extensions;
using VideoPlatform.ElasticSearchService.Infrastructure.Extensions;
using VideoPlatform.ExternalService.Infrastructure.Extensions;
using VideoPlatform.MessageService.Infrastructure.Extensions;
using VideoPlatform.SchedulerService.Infrastructure.Extensions;

namespace VideoPlatform.BLL.Infrastructure.Extensions
{
    public static class BusinessInfrastructureExtension
    {
        public static IServiceCollection AddBusinessInfrastructureConfiguration(this IServiceCollection services, IConfigurationRoot appConfiguration)
        {
            services.AddOptions();

            services.AddDatabaseConfiguration(appConfiguration);

            services.AddRepositoriesCollection();
            services.AddManagersCollection();

            services.AddCQRS(AppDomain.CurrentDomain.Load("VideoPlatform.CQRS"));

            services.AddElasticSearchConfiguration(appConfiguration);
            services.AddIndexingManagersCollection();

            services.AddSettingsConfiguration(appConfiguration);
            services.AddCacheConfiguration(appConfiguration);
            services.AddCacheService();

            services.AddSchedulerConfiguration();

            services.AddMessenger(appConfiguration);
            services.AddEventBus(appConfiguration);
            services.RegisteringEventHandlers();

            services.AddKafkaMessenger(appConfiguration);

            services.AddExternalServicesCollection(appConfiguration);
            services.AddMachineLearningConfiguration(appConfiguration);

            return services;
        }
    }
}