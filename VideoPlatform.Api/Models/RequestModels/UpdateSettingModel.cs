using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.RequestModels;

/// <summary>
///     UpdateSettingModel
/// </summary>
public class UpdateSettingModel
{
    /// <summary>
    ///     Id
    /// </summary>
    [JsonProperty("id")]
    [Required]
    public short Id { get; set; }

    /// <summary>
    ///     Name
    /// </summary>
    [JsonProperty("name")]
    [Required]
    public string Name { get; set; }

    /// <summary>
    ///     Value
    /// </summary>
    [JsonProperty("value")]
    [Required]
    public string Value { get; set; }
}