using System.Collections.Generic;
using Microsoft.ML.Data;
using VideoPlatform.AIL.Models.TripModels;

namespace VideoPlatform.BLL.Interfaces;

public interface IRegressionManager
{
    RegressionMetrics BuildRegressionModel();

    ICollection<TripFarePredictionModel> CalculatePrediction(IEnumerable<TripModel> items, bool rebuildModel = false);
}