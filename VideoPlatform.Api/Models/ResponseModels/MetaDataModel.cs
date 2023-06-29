using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.Api.Models.ResponseModels;

/// <summary>
///     MetaDataModel
/// </summary>
public class MetaDataModel
{
    /// <summary>
    ///     Id
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    ///     Name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Description
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Value
    /// </summary>
    [JsonProperty("value")]
    public string Value { get; set; }

    /// <summary>
    ///     Type
    /// </summary>
    [JsonProperty("type")]
    [EnumDataType(typeof(MetaType))]
    [JsonConverter(typeof(StringEnumConverter))]
    public MetaType Type { get; set; }

    /// <summary>
    ///     CreatedAt
    /// </summary>
    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     UpdatedAt
    /// </summary>
    [JsonProperty("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}