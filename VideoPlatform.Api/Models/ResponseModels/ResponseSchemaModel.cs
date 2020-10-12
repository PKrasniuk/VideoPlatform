using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.ResponseModels
{
    /// <summary>
    /// ResponseSchemaModel
    /// </summary>
    public class ResponseSchemaModel
    {
        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty(propertyName: "type")]
        public string Type { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty(propertyName: "title")]
        public string Title { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty(propertyName: "status")]
        public int Status { get; set; }

        /// <summary>
        /// Detail
        /// </summary>
        [JsonProperty(propertyName: "detail")]
        public string Detail { get; set; }

        /// <summary>
        /// Instance
        /// </summary>
        [JsonProperty(propertyName: "instance")]
        public string Instance { get; set; }
    }
}