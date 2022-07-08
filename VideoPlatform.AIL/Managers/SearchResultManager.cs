using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Data;
using VideoPlatform.AIL.Models.SearchResultModels;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Common.Infrastructure.Helpers;

namespace VideoPlatform.AIL.Managers
{
    public class SearchResultManager : IManager<SearchResultModel, SearchResultPredictionModel, RankingMetrics>
    {
        private static string _dataSetsPath;
        private static string _modelsPath;
        private bool _trainDataSetFileExists;
        private bool _validationDataSetFileExists;
        private bool _testDataSetFileExists;

        private readonly MLContext _mlContext;

        public SearchResultManager(string dataSetsPath, string modelsPath)
        {
            _dataSetsPath = PathHelper.GetAbsolutePath(dataSetsPath);
            _modelsPath = PathHelper.GetAbsolutePath(modelsPath);

            //Create ML Context with seed for repeteable/deterministic results
            _mlContext = new MLContext(seed: 0);

            _trainDataSetFileExists = false;
            _validationDataSetFileExists = false;
            _testDataSetFileExists = false;
        }

        private static string TrainDataSetPath => $"{_dataSetsPath}/MSLRWeb10KTrain720kRows.tsv";
        private static string ValidationDataSetPath => $"{_dataSetsPath}/MSLRWeb10KValidate240kRows.tsv";
        private static string TestDataSetPath => $"{_dataSetsPath}/MSLRWeb10KTest240kRows.tsv";

        private static string ModelPath => $"{_modelsPath}/RankingModel.zip";

        public Tuple<ITransformer, RankingMetrics> BuildTrainEvaluateAndSaveModel()
        {
            var timeStart = DateTime.Now;

            PrepareDataSets();

            while (!_trainDataSetFileExists || !_validationDataSetFileExists || !_testDataSetFileExists)
            {
                if (timeStart.AddSeconds(ExternalServiceConstants.MaxSecondWaitingDataDownload) < DateTime.Now)
                {
                    throw new Exception("Can not download data files to build a model");
                }
            }

            // Create the pipeline using the training dataSets schema;
            // the validation and testing data have the same schema.
            var trainData = _mlContext.Data.LoadFromTextFile<SearchResultModel>(TrainDataSetPath, '\t', true);
            var pipeline = CreatePipeline(trainData);

            // Train the model on the training dataSet. To perform training you need to call the Fit() method.
            var model = pipeline.Fit(trainData);

            // Evaluate the model using the metrics from the validation dataSet;
            // you would then retrain and reevaluate the model until the desired metrics are achieved.
            var validationData = _mlContext.Data.LoadFromTextFile<SearchResultModel>(ValidationDataSetPath);
            EvaluateModel(model, validationData);

            // Combine the training and validation dataSets.
            var validationDataEnum = _mlContext.Data.CreateEnumerable<SearchResultModel>(validationData, false);
            var trainDataEnum = _mlContext.Data.CreateEnumerable<SearchResultModel>(trainData, false);
            var trainValidationDataEnum = validationDataEnum.Concat(trainDataEnum).ToList();
            var trainValidationData = _mlContext.Data.LoadFromEnumerable(trainValidationDataEnum);

            // Train the model on the train + validation dataSet.
            model = pipeline.Fit(trainValidationData);

            // Evaluate the model using the metrics from the testing dataSet;
            // you do this only once and these are your final metrics.
            var testData = _mlContext.Data.LoadFromTextFile<SearchResultModel>(TestDataSetPath);
            EvaluateModel(model, testData);

            // Combine the training, validation, and testing dataSets.
            var testDataEnum = _mlContext.Data.CreateEnumerable<SearchResultModel>(testData, false);
            var allDataEnum = trainValidationDataEnum.Concat(testDataEnum).ToList();
            var allData = _mlContext.Data.LoadFromEnumerable(allDataEnum);

            // Retrain the model on all of the data, train + validate + test.
            model = pipeline.Fit(allData);
            var metrics = EvaluateModel(model, allData);

            // Save the model.
            if (!Directory.Exists(_modelsPath))
            {
                Directory.CreateDirectory(_modelsPath);
            }
            _mlContext.Model.Save(model, null, ModelPath);

            return new Tuple<ITransformer, RankingMetrics>(model, metrics);
        }

        public ICollection<SearchResultPredictionModel> CalculatePrediction(IEnumerable<SearchResultModel> items, bool rebuildModel = false)
        {
            if (!File.Exists(ModelPath) || rebuildModel)
            {
                BuildTrainEvaluateAndSaveModel();
            }

            if (!File.Exists(ModelPath))
            {
                return null;
            }

            // Load the model to perform predictions with it.
            var predictionPipeline = _mlContext.Model.Load(ModelPath, out _);

            // Predict rankings.
            var predictions = predictionPipeline.Transform(_mlContext.Data.LoadFromEnumerable(items));

            // In the predictions, get the scores of the search results included in the first query (e.g. group).
            var searchQueries =
                _mlContext.Data.CreateEnumerable<SearchResultPredictionModel>(predictions, reuseRowObject: false).ToList();
            var firstGroupId = searchQueries.First().GroupId;
            return searchQueries.Take(100).Where(p => p.GroupId == firstGroupId).OrderByDescending(p => p.Score).ToList();
        }

        private void PrepareDataSets()
        {
            const string trainDataSetUrl = "https://aka.ms/mlnet-resources/benchmarks/MSLRWeb10KTrain720kRows.tsv";
            const string validationDataSetUrl = "https://aka.ms/mlnet-resources/benchmarks/MSLRWeb10KValidate240kRows.tsv";
            const string testDataSetUrl = "https://aka.ms/mlnet-resources/benchmarks/MSLRWeb10KTest240kRows.tsv";

            if (!Directory.Exists(_dataSetsPath))
            {
                Directory.CreateDirectory(_dataSetsPath);
            }

            if (!Directory.Exists(_modelsPath))
            {
                Directory.CreateDirectory(_modelsPath);
            }

            if (!File.Exists(TrainDataSetPath))
                AsyncHelper.RunSync(async () => await DownloadFileAsync(new Uri(trainDataSetUrl), TrainDataSetPath));

            _trainDataSetFileExists = true;

            if (!File.Exists(ValidationDataSetPath))
                AsyncHelper.RunSync(async () =>
                    await DownloadFileAsync(new Uri(validationDataSetUrl), ValidationDataSetPath));

            _validationDataSetFileExists = true;

            if (!File.Exists(TestDataSetPath))
                AsyncHelper.RunSync(async () => await DownloadFileAsync(new Uri(testDataSetUrl), TestDataSetPath));

            _testDataSetFileExists = true;
        }

        private static async Task DownloadFileAsync(Uri uri, string outputPath)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(uri);
            await using var fs = new FileStream(outputPath, FileMode.CreateNew);
            await response.Content.CopyToAsync(fs);
        }

        private IEstimator<ITransformer> CreatePipeline(IDataView dataView)
        {
            const string featuresVectorName = "Features";

            // Specify the columns to include in the feature input data.
            var featureCols = dataView.Schema.AsQueryable()
                .Select(s => s.Name)
                .Where(c =>
                    c != nameof(SearchResultModel.Label) &&
                    c != nameof(SearchResultModel.GroupId))
                .ToArray();

            // Create an Estimator and transform the data:
            // 1. Concatenate the feature columns into a single Features vector.
            // 2. Create a key type for the label input data by using the value to key transform.
            // 3. Create a key type for the group input data by using a hash transform.
            IEstimator<ITransformer> dataPipeline = _mlContext.Transforms.Concatenate(featuresVectorName, featureCols)
                .Append(_mlContext.Transforms.Conversion.MapValueToKey(nameof(SearchResultModel.Label)))
                .Append(_mlContext.Transforms.Conversion.Hash(nameof(SearchResultModel.GroupId),
                    nameof(SearchResultModel.GroupId), 20));

            // Set the LightGBM LambdaRank trainer.
            IEstimator<ITransformer> trainer = _mlContext.Ranking.Trainers.LightGbm();
            return dataPipeline.Append(trainer);
        }

        private RankingMetrics EvaluateModel(ITransformer model, IDataView data)
        {
            // Use the model to perform predictions on the test data.
            var predictions = model.Transform(data);

            // Evaluate the metrics for the data using NDCG; by default,
            // metrics for the up to 3 search results in the query are reported (e.g. NDCG@3).
            return _mlContext.Ranking.Evaluate(predictions);
        }
    }
}