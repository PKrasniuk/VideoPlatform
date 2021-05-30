using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VideoPlatform.Api.Models.Enums;

namespace VideoPlatform.Api.Models.RequestModels
{
    /// <summary>
    /// FilterPartnerModel
    /// </summary>
    public class FilterPartnerModel : FilterModel
    {
        /// <summary>
        /// SortedProperty
        /// </summary>
        [EnumDataType(typeof(PartnerSortedProperty))]
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("sortedProperty")]
        [DefaultValue(PartnerSortedProperty.Id)]
        [Required]
        public PartnerSortedProperty SortedProperty { get; set; }
    }
}
