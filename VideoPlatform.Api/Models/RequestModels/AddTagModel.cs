using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.RequestModels;

/// <summary>
///     AddTagModel
/// </summary>
public class AddTagModel
{
    /// <summary>
    ///     Name
    /// </summary>
    [JsonProperty("name")]
    [Required]
    public string Name { get; set; }
}