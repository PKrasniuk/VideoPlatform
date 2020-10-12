using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.ResponseModels
{
    /// <summary>
    /// OutputSearchResultPredictionModel
    /// </summary>
    public class OutputSearchResultPredictionModel
    {
        /// <summary>
        /// GroupId
        /// </summary>
        [JsonProperty(propertyName: "groupId")]
        public uint GroupId { get; set; }

        /// <summary>
        /// Label
        /// </summary>
        [JsonProperty(propertyName: "label")]
        public uint Label { get; set; }

        /// <summary>
        /// Score
        /// </summary>
        [JsonProperty(propertyName: "score")]
        public float Score { get; set; }

        /// <summary>
        /// Features
        /// </summary>
        [JsonProperty(propertyName: "features")]
        public float[] Features { get; set; }
    }
}