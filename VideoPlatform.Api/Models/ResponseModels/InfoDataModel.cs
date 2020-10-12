using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.Api.Models.ResponseModels
{
    /// <summary>
    /// InfoDataModel
    /// </summary>
    public class InfoDataModel
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty(propertyName: "id")]
        public string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty(propertyName: "name")]
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty(propertyName: "description")]
        public string Description { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        [JsonProperty(propertyName: "value")]
        public string Value { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty(propertyName: "type")]
        [EnumDataType(typeof(InfoType))]
        [JsonConverter(typeof(StringEnumConverter))]
        public InfoType Type { get; set; }
    }
}