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
        public static IServiceCollection AddBusinessInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            services.AddDatabaseConfiguration(configuration);

            services.AddRepositoriesCollection();
            services.AddManagersCollection();

            services.AddCQRS(AppDomain.CurrentDomain.Load("VideoPlatform.CQRS"));

            services.AddElasticSearchConfiguration(configuration);
            services.AddIndexingManagersCollection();

            services.AddSettingsConfiguration(configuration);
            services.AddCacheConfiguration(configuration);
            services.AddCacheService();

            services.AddSchedulerConfiguration();

            services.AddMessenger(configuration);
            services.AddEventBus(configuration);
            services.RegisteringEventHandlers();

            services.AddKafkaMessenger(configuration);

            services.AddExternalServicesCollection(configuration);
            services.AddMachineLearningConfiguration(configuration);

            return services;
        }
    }
}