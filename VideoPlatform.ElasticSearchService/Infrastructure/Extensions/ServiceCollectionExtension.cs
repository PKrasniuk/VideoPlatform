using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.ElasticSearchService.Interfaces;
using VideoPlatform.ElasticSearchService.Managers;

namespace VideoPlatform.ElasticSearchService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddIndexingManagersCollection(this IServiceCollection services)
        {
            services.AddTransient<IIndexingExperimentManager, IndexingExperimentManager>();
            services.AddTransient<IIndexingFavoriteManager, IndexingFavoriteManager>();
            services.AddTransient<IIndexingMediaManager, IndexingMediaManager>();
            services.AddTransient<IIndexingMediaTagManager, IndexingMediaTagManager>();
            services.AddTransient<IIndexingPartnerManager, IndexingPartnerManager>();
            services.AddTransient<IIndexingPartnerMediaManager, IndexingPartnerMediaManager>();
            services.AddTransient<IIndexingPartnerTypesManager, IndexingPartnerTypesManager>();
            services.AddTransient<IIndexingPlaylistManager, IndexingPlaylistManager>();
            services.AddTransient<IIndexingPlaylistMediaManager, IndexingPlaylistMediaManager>();
            services.AddTransient<IIndexingSeriesManager, IndexingSeriesManager>();
            services.AddTransient<IIndexingSettingManager, IndexingSettingManager>();
            services.AddTransient<IIndexingSubscriptionSeriesManager, IndexingSubscriptionSeriesManager>();
            services.AddTransient<IIndexingSubscriptionTopicManager, IndexingSubscriptionTopicManager>();
            services.AddTransient<IIndexingTagManager, IndexingTagManager>();
            services.AddTransient<IIndexingToolManager, IndexingToolManager>();
            services.AddTransient<IIndexingTopicManager, IndexingTopicManager>();

            return services;
        }
    }
}