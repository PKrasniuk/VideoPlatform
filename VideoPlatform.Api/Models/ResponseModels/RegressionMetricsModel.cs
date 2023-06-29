using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.ResponseModels;

/// <summary>
///     RegressionMetricsModel
/// </summary>
public class RegressionMetricsModel
{
    /// <summary>
    ///     MeanAbsoluteError
    /// </summary>
    [JsonProperty("meanAbsoluteError")]
    public double MeanAbsoluteError { get; set; }

    /// <summary>
    ///     MeanSquaredError
    /// </summary>
    [JsonProperty("meanSquaredError")]
    public double MeanSquaredError { get; set; }

    /// <summary>
    ///     RootMeanSquaredError
    /// </summary>
    [JsonProperty("rootMeanSquaredError")]
    public double RootMeanSquaredError { get; set; }

    /// <summary>
    ///     LossFunction
    /// </summary>
    [JsonProperty("lossFunction")]
    public double LossFunction { get; set; }

    /// <summary>
    ///     RSquared
    /// </summary>
    [JsonProperty("rSquared")]
    public double RSquared { get; set; }
}