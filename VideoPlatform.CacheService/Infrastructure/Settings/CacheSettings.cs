namespace VideoPlatform.CacheService.Infrastructure.Settings;

/// <summary>
///     CacheSettings
/// </summary>
public class CacheSettings
{
    /// <summary>
    ///     RedisUrl
    /// </summary>
    public string RedisUrl { get; set; }

    /// <summary>
    ///     InstanceName
    /// </summary>
    public string InstanceName { get; set; }

    /// <summary>
    ///     PartnersExpirationMinutes
    /// </summary>
    public int PartnersExpirationMinutes { get; set; }
}