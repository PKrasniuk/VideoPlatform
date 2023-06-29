using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.ResponseModels;

/// <summary>
///     ResponseSchemaModel
/// </summary>
public class ResponseSchemaModel
{
    /// <summary>
    ///     Type
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    ///     Title
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; }

    /// <summary>
    ///     Status
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    ///     Detail
    /// </summary>
    [JsonProperty("detail")]
    public string Detail { get; set; }

    /// <summary>
    ///     Instance
    /// </summary>
    [JsonProperty("instance")]
    public string Instance { get; set; }
}