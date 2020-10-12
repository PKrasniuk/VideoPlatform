using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.ResponseModels
{
    /// <summary>
    /// TagModel
    /// </summary>
    public class TagModel
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty(propertyName: "id")]
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty(propertyName: "name")]
        public string Name { get; set; }
    }
}