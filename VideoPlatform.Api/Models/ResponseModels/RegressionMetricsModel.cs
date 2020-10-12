using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.ResponseModels
{
    /// <summary>
    /// RegressionMetricsModel
    /// </summary>
    public class RegressionMetricsModel
    {
        /// <summary>
        /// MeanAbsoluteError
        /// </summary>
        [JsonProperty(propertyName: "meanAbsoluteError")]
        public double MeanAbsoluteError { get; set; }

        /// <summary>
        /// MeanSquaredError
        /// </summary>
        [JsonProperty(propertyName: "meanSquaredError")]
        public double MeanSquaredError { get; set; }

        /// <summary>
        /// RootMeanSquaredError
        /// </summary>
        [JsonProperty(propertyName: "rootMeanSquaredError")]
        public double RootMeanSquaredError { get; set; }

        /// <summary>
        /// LossFunction
        /// </summary>
        [JsonProperty(propertyName: "lossFunction")]
        public double LossFunction { get; set; }

        /// <summary>
        /// RSquared
        /// </summary>
        [JsonProperty(propertyName: "rSquared")]
        public double RSquared { get; set; }
    }
}