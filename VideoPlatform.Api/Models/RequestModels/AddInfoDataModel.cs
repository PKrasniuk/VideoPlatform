using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.Api.Models.RequestModels
{
    /// <summary>
    /// AddInfoDataModel
    /// </summary>
    public class AddInfoDataModel
    {
        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("description")]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        [JsonProperty("value")]
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("type")]
        [EnumDataType(typeof(InfoType))]
        [JsonConverter(typeof(StringEnumConverter))]
        [DefaultValue(InfoType.BaseDocument)]
        [Required]
        public InfoType Type { get; set; } = InfoType.BaseDocument;
    }
}