namespace VideoPlatform.Common.Infrastructure.Constants;

/// <summary>
///     Constants
/// </summary>
public static class ConfigurationConstants
{
    /// <summary>
    ///     StaticCacheSeconds
    /// </summary>
    public const int StaticCacheSeconds = 24 * 60 * 60;

    /// <summary>
    ///     SecurityDefinitionName
    /// </summary>
    public const string SecurityDefinitionName = "oauth2";

    /// <summary>
    ///     ConnectionStringName
    /// </summary>
    public const string ConnectionStringName = "Default";

    /// <summary>
    ///     MetaDataAccessName
    /// </summary>
    public const string MetaDataAccessName = "MetaDataAccess";

    /// <summary>
    ///     CosmosDataAccessName
    /// </summary>
    public const string CosmosDataAccessName = "CosmosDataAccess";

    /// <summary>
    ///     DefaultExpirationMinutes
    /// </summary>
    public const int DefaultExpirationMinutes = 120;

    /// <summary>
    ///     NotificationGroupName
    /// </summary>
    public const string NotificationGroupName = "videoPlatformNotification";

    /// <summary>
    ///     MaxNumAttempts
    /// </summary>
    public const int MaxNumAttempts = 20;

    /// <summary>
    ///     DefaultCorsPolicyName
    /// </summary>
    public const string DefaultCorsPolicyName = "localhost";
}