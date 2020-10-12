using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.Api.Models.RequestModels
{
    /// <summary>
    /// UpdateMetaDataModel
    /// </summary>
    public class UpdateMetaDataModel
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty(propertyName: "id")]
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty(propertyName: "name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty(propertyName: "description")]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        [JsonProperty(propertyName: "value")]
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty(propertyName: "type")]
        [EnumDataType(typeof(MetaType))]
        [JsonConverter(typeof(StringEnumConverter))]
        [DefaultValue(MetaType.Document)]
        [Required]
        public MetaType Type { get; set; } = MetaType.Document;
    }
}