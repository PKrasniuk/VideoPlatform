using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ML.Data;
using VideoPlatform.AIL.Managers;
using VideoPlatform.AIL.Models.SearchResultModels;
using VideoPlatform.AIL.Models.TripModels;

namespace VideoPlatform.AIL.Infrastructure.Extensions;

public static class MachineLearningExtension
{
    public static void AddMachineLearningConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IManager<TripModel, TripFarePredictionModel, RegressionMetrics>, TripManager>(_ =>
            new TripManager(configuration["AIConfiguration:Trip:DataSetsPath"],
                configuration["AIConfiguration:Trip:ModelsPath"]));

        services.AddTransient<IManager<SearchResultModel, SearchResultPredictionModel, RankingMetrics>,
            SearchResultManager>(_ =>
            new SearchResultManager(configuration["AIConfiguration:SearchResult:DataSetsPath"],
                configuration["AIConfiguration:SearchResult:ModelsPath"]));
    }
}