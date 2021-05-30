using Newtonsoft.Json;

namespace VideoPlatform.Common.Models.ResponseModels
{
    /// <summary>
    /// ErrorDetailsModel
    /// </summary>
    public class ErrorDetailsModel
    {
        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Errors
        /// </summary>
        [JsonProperty("errors")]
        public string Errors { get; set; }

        /// <summary>
        /// TraceId
        /// </summary>
        [JsonProperty("traceId")]
        public string TraceId { get; set; }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}