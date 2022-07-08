using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.BLL.Managers;

namespace VideoPlatform.BLL.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddManagersCollection(this IServiceCollection services)
        {
            services.AddTransient<IExperimentManager, ExperimentManager>();
            services.AddTransient<IFavoriteManager, FavoriteManager>();
            services.AddTransient<IMediaManager, MediaManager>();
            services.AddTransient<IMediaTagManager, MediaTagManager>();
            services.AddTransient<IPartnerManager, PartnerManager>();
            services.AddTransient<IPartnerMediaManager, PartnerMediaManager>();
            services.AddTransient<IPartnerTypesManager, PartnerTypesManager>();
            services.AddTransient<IPlaylistManager, PlaylistManager>();
            services.AddTransient<IPlaylistMediaManager, PlaylistMediaManager>();
            services.AddTransient<ISeriesManager, SeriesManager>();
            services.AddTransient<ISettingManager, SettingManager>();
            services.AddTransient<ISubscriptionSeriesManager, SubscriptionSeriesManager>();
            services.AddTransient<ISubscriptionTopicManager, SubscriptionTopicManager>();
            services.AddTransient<ITagManager, TagManager>();
            services.AddTransient<IToolManager, ToolManager>();
            services.AddTransient<ITopicManager, TopicManager>();
            services.AddTransient<IFileOperationManager, FileOperationManager>();
            services.AddTransient<IUserRolesManager, UserRolesManager>();

            services.AddTransient<IMetaDataManager, MetaDataManager>();

            services.AddTransient<IInfoDataManager, InfoDataManager>();

            services.AddTransient<IRegressionManager, RegressionManager>();
            services.AddTransient<IRankingManager, RankingManager>();
        }
    }
}