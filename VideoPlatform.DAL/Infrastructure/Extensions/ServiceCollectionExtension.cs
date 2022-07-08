using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.DAL.Interfaces;
using VideoPlatform.DAL.Repositories;

namespace VideoPlatform.DAL.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddRepositoriesCollection(this IServiceCollection services)
        {
            services.AddTransient<IExperimentsRepository, ExperimentsRepository>();
            services.AddTransient<IFavoritesRepository, FavoritesRepository>();
            services.AddTransient<IMediaRepository, MediaRepository>();
            services.AddTransient<IMediaTagsRepository, MediaTagsRepository>();
            services.AddTransient<IPartnerMediaRepository, PartnerMediaRepository>();
            services.AddTransient<IPartnersRepository, PartnersRepository>();
            services.AddTransient<IPartnerTypesRepository, PartnerTypesRepository>();
            services.AddTransient<IPlaylistMediaRepository, PlaylistMediaRepository>();
            services.AddTransient<IPlaylistsRepository, PlaylistsRepository>();
            services.AddTransient<ISeriesRepository, SeriesRepository>();
            services.AddTransient<ISettingsRepository, SettingsRepository>();
            services.AddTransient<ISubscriptionSeriesRepository, SubscriptionSeriesRepository>();
            services.AddTransient<ISubscriptionTopicsRepository, SubscriptionTopicsRepository>();
            services.AddTransient<ITagsRepository, TagsRepository>();
            services.AddTransient<IToolsRepository, ToolsRepository>();
            services.AddTransient<ITopicsRepository, TopicsRepository>();
            services.AddTransient<IUserRolesRepository, UserRolesRepository>();
            services.AddTransient<IMetaDataRepository, MetaDataRepository>();
            services.AddTransient<IInfoDataRepository, InfoDataRepository>();
        }
    }
}