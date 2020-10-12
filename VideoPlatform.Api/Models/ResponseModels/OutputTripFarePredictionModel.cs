using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.ResponseModels
{
    /// <summary>
    /// OutputTripFarePredictionModel
    /// </summary>
    public class OutputTripFarePredictionModel
    {
        /// <summary>
        /// FareAmount
        /// </summary>
        [JsonProperty(propertyName: "fareAmount")]
        public float FareAmount { get; set; }
    }
}