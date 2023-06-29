namespace VideoPlatform.Api.Models.Settings;

/// <summary>
///     MemoryMetrics
/// </summary>
public class MemoryMetrics
{
    /// <summary>
    ///     Total
    /// </summary>
    public double Total { get; set; }

    /// <summary>
    ///     Used
    /// </summary>
    public double Used { get; set; }

    /// <summary>
    ///     Free
    /// </summary>
    public double Free { get; set; }

    /// <summary>
    ///     Duration
    /// </summary>
    public long Duration { get; set; }
}