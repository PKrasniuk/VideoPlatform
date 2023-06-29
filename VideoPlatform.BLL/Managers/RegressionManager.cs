using System.Collections.Generic;
using Microsoft.ML.Data;
using VideoPlatform.AIL.Managers;
using VideoPlatform.AIL.Models.TripModels;
using VideoPlatform.BLL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class RegressionManager : IRegressionManager
{
    private readonly IManager<TripModel, TripFarePredictionModel, RegressionMetrics> _aiManager;

    public RegressionManager(IManager<TripModel, TripFarePredictionModel, RegressionMetrics> aiManager)
    {
        _aiManager = aiManager;
    }

    public RegressionMetrics BuildRegressionModel()
    {
        return _aiManager.BuildTrainEvaluateAndSaveModel().Item2;
    }

    public ICollection<TripFarePredictionModel> CalculatePrediction(IEnumerable<TripModel> items, bool rebuildModel)
    {
        return _aiManager.CalculatePrediction(items, rebuildModel);
    }
}