using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.Api.Models.ResponseModels
{
    /// <summary>
    /// PartnerTypesModel
    /// </summary>
    public class PartnerTypesModel
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty(propertyName: "id")]
        public int Id { get; set; }

        /// <summary>
        /// PartnerId
        /// </summary>
        [JsonProperty(propertyName: "partnerId")]
        public int PartnerId { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty(propertyName: "type")]
        [EnumDataType(typeof(PartnerType))]
        [JsonConverter(typeof(StringEnumConverter))]
        public PartnerType Type { get; set; }
    }
}