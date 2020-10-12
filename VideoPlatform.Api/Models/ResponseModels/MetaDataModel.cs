using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.Api.Models.ResponseModels
{
    /// <summary>
    /// MetaDataModel
    /// </summary>
    public class MetaDataModel
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
        [EnumDataType(typeof(MetaType))]
        [JsonConverter(typeof(StringEnumConverter))]
        public MetaType Type { get; set; }

        /// <summary>
        /// CreatedAt
        /// </summary>
        [JsonProperty(propertyName: "createdAt")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// UpdatedAt
        /// </summary>
        [JsonProperty(propertyName: "updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}