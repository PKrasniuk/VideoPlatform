using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;
using VideoPlatform.AIL.Models.TripModels;
using VideoPlatform.Common.Infrastructure.Helpers;

namespace VideoPlatform.AIL.Managers;

public class TripManager : IManager<TripModel, TripFarePredictionModel, RegressionMetrics>
{
    private static string _dataSetsPath;
    private static string _modelsPath;

    private readonly MLContext _mlContext;

    public TripManager(string dataSetsPath, string modelsPath)
    {
        _dataSetsPath = PathHelper.GetAbsolutePath(dataSetsPath);
        _modelsPath = PathHelper.GetAbsolutePath(modelsPath);

        //Create ML Context with seed for repeteable/deterministic results
        _mlContext = new MLContext(0);
    }

    private static string TrainDataPath => $"{_dataSetsPath}/fare-train.csv";
    private static string TestDataPath => $"{_dataSetsPath}/fare-test.csv";

    private static string ModelPath => $"{_modelsPath}/FareModel.zip";

    public Tuple<ITransformer, RegressionMetrics> BuildTrainEvaluateAndSaveModel()
    {
        if (!Directory.Exists(_dataSetsPath) || !File.Exists(TrainDataPath) || !File.Exists(TestDataPath)) return null;

        // STEP 1: Common data loading configuration
        var baseTrainingDataView =
            _mlContext.Data.LoadFromTextFile<TripModel>(TrainDataPath, hasHeader: true, separatorChar: ',');
        var testDataView =
            _mlContext.Data.LoadFromTextFile<TripModel>(TestDataPath, hasHeader: true, separatorChar: ',');
        var trainingDataView = _mlContext.Data.FilterRowsByColumn(baseTrainingDataView, nameof(TripModel.FareAmount),
            1, 150);

        // STEP 2: Common data process configuration with pipeline data transformations
        var dataProcessPipeline = _mlContext.Transforms.CopyColumns("Label", nameof(TripModel.FareAmount))
            .Append(_mlContext.Transforms.Categorical.OneHotEncoding("VendorIdEncoded", nameof(TripModel.VendorId)))
            .Append(_mlContext.Transforms.Categorical.OneHotEncoding("RateCodeEncoded", nameof(TripModel.RateCode)))
            .Append(_mlContext.Transforms.Categorical.OneHotEncoding("PaymentTypeEncoded",
                nameof(TripModel.PaymentType)))
            .Append(_mlContext.Transforms.NormalizeMeanVariance(nameof(TripModel.PassengerCount)))
            .Append(_mlContext.Transforms.NormalizeMeanVariance(nameof(TripModel.TripTime)))
            .Append(_mlContext.Transforms.NormalizeMeanVariance(nameof(TripModel.TripDistance)))
            .Append(_mlContext.Transforms.Concatenate("Features", "VendorIdEncoded", "RateCodeEncoded",
                "PaymentTypeEncoded", nameof(TripModel.PassengerCount), nameof(TripModel.TripTime),
                nameof(TripModel.TripDistance)));

        // STEP 3: Set the training algorithm, then create and config the modelBuilder - Selected Trainer (SDCA Regression algorithm)                            
        var trainer = _mlContext.Regression.Trainers.Sdca();
        var trainingPipeline = dataProcessPipeline.Append(trainer);

        // STEP 4: Train the model fitting to the DataSet
        //The pipeline is trained on the dataSet that has been loaded and transformed.
        var trainedModel = trainingPipeline.Fit(trainingDataView);

        // STEP 5: Evaluate the model and calculate accuracy stats
        var predictions = trainedModel.Transform(testDataView);
        var metrics = _mlContext.Regression.Evaluate(predictions);

        // STEP 6: Save/persist the trained model to a .zip file
        if (!Directory.Exists(_modelsPath)) Directory.CreateDirectory(_modelsPath);
        _mlContext.Model.Save(trainedModel, trainingDataView.Schema, ModelPath);

        return new Tuple<ITransformer, RegressionMetrics>(trainedModel, metrics);
    }

    public ICollection<TripFarePredictionModel> CalculatePrediction(IEnumerable<TripModel> items,
        bool rebuildModel = false)
    {
        if (!File.Exists(ModelPath) || rebuildModel) BuildTrainEvaluateAndSaveModel();

        if (!File.Exists(ModelPath)) return null;

        var trainedModel = _mlContext.Model.Load(ModelPath, out _);

        // Create prediction engine related to the loaded trained model
        var predictionEngine =
            _mlContext.Model.CreatePredictionEngine<TripModel, TripFarePredictionModel>(trainedModel);

        var result = new Collection<TripFarePredictionModel>();
        foreach (var item in items) result.Add(predictionEngine.Predict(item));
        return result;
    }
}