using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using VideoPlatform.AIL.Models.SearchResultModels;
using VideoPlatform.AIL.Models.TripModels;
using VideoPlatform.Api.Models.RequestModels;
using VideoPlatform.Api.Models.ResponseModels;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.Common.Infrastructure.Helpers;
using VideoPlatform.Common.Models.ResponseModels;

namespace VideoPlatform.Api.Controllers
{
    /// <summary>
    /// AIController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly IRegressionManager _regressionManager;
        private readonly IRankingManager _rankingManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AIController> _logger;

        /// <summary>
        /// AIController constructor
        /// </summary>
        /// <param name="regressionManager"></param>
        /// <param name="rankingManager"></param>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public AIController(IRegressionManager regressionManager, IRankingManager rankingManager, IMapper mapper,
            IConfiguration configuration, ILogger<AIController> logger)
        {
            _regressionManager = regressionManager ?? throw new ArgumentNullException(nameof(regressionManager));
            _rankingManager = rankingManager ?? throw new ArgumentNullException(nameof(rankingManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Build Trip Regression Model
        /// </summary>
        /// <returns></returns>
        [HttpGet("buildTripRegressionModel")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegressionMetricsModel))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsModel))]
        public async Task<ActionResult<RegressionMetricsModel>> BuildTripRegressionModelAsync()
        {
            await Task.Delay(1);

            _logger.LogInformation("The method BuildTripRegressionModelAsync");

            var result = _regressionManager.BuildRegressionModel();
            if (result != null)
            {
                return _mapper.Map<RegressionMetricsModel>(result);
            }

            return UnprocessableEntity();
        }

        /// <summary>
        /// Build Search Result Ranking Model
        /// </summary>
        /// <returns></returns>
        [HttpGet("buildSearchResultRankingModel")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RankingMetricsModel))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsModel))]
        public async Task<ActionResult<RankingMetricsModel>> BuildSearchResultRankingModelAsync()
        {
            await Task.Delay(1);

            _logger.LogInformation("The method BuildSearchResultRankingModelAsync");

            var result = _rankingManager.BuildRankingModel();
            if (result != null)
            {
                return _mapper.Map<RankingMetricsModel>(result);
            }

            return UnprocessableEntity();
        }

        /// <summary>
        /// Calculate Trip Regression
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("tripRegression")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<OutputTripFarePredictionModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsModel))]
        public async Task<ActionResult<ICollection<OutputTripFarePredictionModel>>> CalculateTripRegressionAsync([FromForm] InputTripModel model)
        {
            await Task.Delay(1);

            _logger.LogInformation("The method CalculateTripRegressionAsync");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _regressionManager.CalculatePrediction(_mapper.Map<ICollection<TripModel>>(new List<InputTripModel>
            {
                model
            }), model.RebuildModel);

            if (result != null && result.Any())
            {
                return _mapper.Map<Collection<OutputTripFarePredictionModel>>(result);
            }

            return UnprocessableEntity();
        }

        /// <summary>
        /// Calculate Search Result Ranking
        /// </summary>
        /// <returns></returns>
        [HttpGet("searchResultRanking")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<OutputSearchResultPredictionModel>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsModel))]
        public async Task<ActionResult<ICollection<OutputSearchResultPredictionModel>>> CalculateSearchResultRankingAsync()
        {
            await Task.Delay(1);

            _logger.LogInformation("The method CalculateSearchResultRankingAsync");

            // Prepare data from test data
            // ------------------------------------
            var dataSetPath = $"{PathHelper.GetAbsolutePath(_configuration["AIConfiguration:SearchResult:DataSetsPath"])}/MSLRWeb10KTest240kRows.tsv";
            var mlContext = new MLContext(seed: 0);
            var data = mlContext.Data.CreateEnumerable<SearchResultModel>(
                mlContext.Data.LoadFromTextFile<SearchResultModel>(dataSetPath), false);
            // ------------------------------------

            var result = _rankingManager.CalculatePrediction(data);
            if (result != null)
            {
                return _mapper.Map<Collection<OutputSearchResultPredictionModel>>(result);
            }

            return UnprocessableEntity();
        }
    }
}