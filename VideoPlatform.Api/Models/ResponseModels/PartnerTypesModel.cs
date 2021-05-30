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
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// PartnerId
        /// </summary>
        [JsonProperty("partnerId")]
        public int PartnerId { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("type")]
        [EnumDataType(typeof(PartnerType))]
        [JsonConverter(typeof(StringEnumConverter))]
        public PartnerType Type { get; set; }
    }
}