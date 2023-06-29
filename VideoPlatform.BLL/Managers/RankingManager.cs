using System.Collections.Generic;
using Microsoft.ML.Data;
using VideoPlatform.AIL.Managers;
using VideoPlatform.AIL.Models.SearchResultModels;
using VideoPlatform.BLL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class RankingManager : IRankingManager
{
    private readonly IManager<SearchResultModel, SearchResultPredictionModel, RankingMetrics> _rankingManager;

    public RankingManager(IManager<SearchResultModel, SearchResultPredictionModel, RankingMetrics> rankingManager)
    {
        _rankingManager = rankingManager;
    }

    public RankingMetrics BuildRankingModel()
    {
        return _rankingManager.BuildTrainEvaluateAndSaveModel().Item2;
    }

    public ICollection<SearchResultPredictionModel> CalculatePrediction(IEnumerable<SearchResultModel> items,
        bool rebuildModel)
    {
        return _rankingManager.CalculatePrediction(items, rebuildModel);
    }
}