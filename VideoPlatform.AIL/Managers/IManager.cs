﻿using System;
using System.Collections.Generic;
using Microsoft.ML;

namespace VideoPlatform.AIL.Managers;

public interface IManager<in TInputModel, TOutputModel, TMetricsModel>
{
    Tuple<ITransformer, TMetricsModel> BuildTrainEvaluateAndSaveModel();

    ICollection<TOutputModel> CalculatePrediction(IEnumerable<TInputModel> items, bool rebuildModel = false);
}