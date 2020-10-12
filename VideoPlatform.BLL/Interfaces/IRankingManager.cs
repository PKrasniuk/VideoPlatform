using System.Collections.Generic;
using Microsoft.ML.Data;
using VideoPlatform.AIL.Models.SearchResultModels;

namespace VideoPlatform.BLL.Interfaces
{
    public interface IRankingManager
    {
        RankingMetrics BuildRankingModel();

        ICollection<SearchResultPredictionModel> CalculatePrediction(IEnumerable<SearchResultModel> items, bool rebuildModel = false);
    }
}