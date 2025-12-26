using System.Collections.Generic;
using Microsoft.ML.Data;
using VideoPlatform.AIL.Managers;
using VideoPlatform.AIL.Models.SearchResultModels;
using VideoPlatform.BLL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class RankingManager(IManager<SearchResultModel, SearchResultPredictionModel, RankingMetrics> rankingManager)
    : IRankingManager
{
    public RankingMetrics BuildRankingModel()
    {
        return rankingManager.BuildTrainEvaluateAndSaveModel().Item2;
    }

    public ICollection<SearchResultPredictionModel> CalculatePrediction(IEnumerable<SearchResultModel> items,
        bool rebuildModel)
    {
        return rankingManager.CalculatePrediction(items, rebuildModel);
    }
}