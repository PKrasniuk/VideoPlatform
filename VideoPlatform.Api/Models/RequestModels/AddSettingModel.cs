using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.RequestModels
{
    /// <summary>
    /// AddSettingModel
    /// </summary>
    public class AddSettingModel
    {
        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        [JsonProperty("value")]
        [Required]
        public string Value { get; set; }
    }
}