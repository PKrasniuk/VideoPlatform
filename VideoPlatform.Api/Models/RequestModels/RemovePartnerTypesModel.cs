using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.Api.Models.RequestModels
{
    /// <summary>
    /// RemovePartnerTypesModel
    /// </summary>
    public class RemovePartnerTypesModel
    {
        /// <summary>
        /// PartnerId
        /// </summary>
        [JsonProperty(propertyName: "partnerId")]
        [Required]
        public int PartnerId { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [EnumDataType(typeof(PartnerType))]
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(propertyName: "type")]
        [DefaultValue(PartnerType.Content)]
        [Required]
        public PartnerType Type { get; set; } = PartnerType.Content;
    }
}