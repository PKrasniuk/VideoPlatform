using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.ResponseModels
{
    /// <summary>
    /// SettingModel
    /// </summary>
    public class SettingModel
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}