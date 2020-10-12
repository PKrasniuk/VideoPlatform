using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.RequestModels
{
    /// <summary>
    /// UpdateTagModel
    /// </summary>
    public class UpdateTagModel
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty(propertyName: "id")]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty(propertyName: "name")]
        [Required]
        public string Name { get; set; }
    }
}