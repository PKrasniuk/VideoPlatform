using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.RequestModels;

/// <summary>
///     RemoveTagsModel
/// </summary>
public class RemoveTagsModel
{
    /// <summary>
    ///     Ids
    /// </summary>
    [JsonProperty("ids")]
    [Required]
    public IList<int> Ids { get; set; }
}