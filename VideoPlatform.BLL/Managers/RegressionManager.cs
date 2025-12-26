using System.Collections.Generic;
using Microsoft.ML.Data;
using VideoPlatform.AIL.Managers;
using VideoPlatform.AIL.Models.TripModels;
using VideoPlatform.BLL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class RegressionManager(IManager<TripModel, TripFarePredictionModel, RegressionMetrics> aiManager)
    : IRegressionManager
{
    public RegressionMetrics BuildRegressionModel()
    {
        return aiManager.BuildTrainEvaluateAndSaveModel().Item2;
    }

    public ICollection<TripFarePredictionModel> CalculatePrediction(IEnumerable<TripModel> items, bool rebuildModel)
    {
        return aiManager.CalculatePrediction(items, rebuildModel);
    }
}