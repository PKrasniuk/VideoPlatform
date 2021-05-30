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
        [JsonProperty("groupId")]
        public uint GroupId { get; set; }

        /// <summary>
        /// Label
        /// </summary>
        [JsonProperty("label")]
        public uint Label { get; set; }

        /// <summary>
        /// Score
        /// </summary>
        [JsonProperty("score")]
        public float Score { get; set; }

        /// <summary>
        /// Features
        /// </summary>
        [JsonProperty("features")]
        public float[] Features { get; set; }
    }
}