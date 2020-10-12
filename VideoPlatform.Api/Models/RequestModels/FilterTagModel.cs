using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VideoPlatform.Api.Models.Enums;

namespace VideoPlatform.Api.Models.RequestModels
{
    /// <summary>
    /// FilterTagModel
    /// </summary>
    public class FilterTagModel : FilterModel
    {
        /// <summary>
        /// SortedProperty
        /// </summary>
        [EnumDataType(typeof(TagSortedProperty))]
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(propertyName: "sortedProperty")]
        [DefaultValue(TagSortedProperty.Id)]
        [Required]
        public TagSortedProperty SortedProperty { get; set; }
    }
}