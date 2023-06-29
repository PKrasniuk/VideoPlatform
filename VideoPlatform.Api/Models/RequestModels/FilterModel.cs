using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Common.Models.Enums;

namespace VideoPlatform.Api.Models.RequestModels;

/// <summary>
///     FilterModel
/// </summary>
public class FilterModel
{
    /// <summary>
    ///     PageNumber
    /// </summary>
    [JsonProperty("pageNumber")]
    [Required]
    [DefaultValue(FilterConstants.DefaultPageNumber)]
    public int PageNumber { get; set; } = FilterConstants.DefaultPageNumber;

    /// <summary>
    ///     PageSize
    /// </summary>
    [JsonProperty("pageSize")]
    [Required]
    [DefaultValue(FilterConstants.DefaultPageSize)]
    public int PageSize { get; set; } = FilterConstants.DefaultPageSize;

    /// <summary>
    ///     SortOrder
    /// </summary>
    [EnumDataType(typeof(SortOrder))]
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty("sortOrder")]
    [DefaultValue(SortOrder.Ascending)]
    [Required]
    public SortOrder SortOrder { get; set; } = SortOrder.Ascending;

    /// <summary>
    ///     FilterQuery
    /// </summary>
    [JsonProperty("filterQuery")]
    public string FilterQuery { get; set; }
}