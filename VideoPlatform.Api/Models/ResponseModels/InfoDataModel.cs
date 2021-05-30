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
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("type")]
        [EnumDataType(typeof(InfoType))]
        [JsonConverter(typeof(StringEnumConverter))]
        public InfoType Type { get; set; }
    }
}