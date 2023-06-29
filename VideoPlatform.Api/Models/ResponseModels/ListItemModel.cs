using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.ResponseModels;

/// <summary>
///     ListItemModel
/// </summary>
/// <typeparam name="T"></typeparam>
public class ListItemModel<T>
{
    /// <summary>
    ///     Id
    /// </summary>
    [JsonProperty("id")]
    public T Id { get; set; }

    /// <summary>
    ///     Name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }
}