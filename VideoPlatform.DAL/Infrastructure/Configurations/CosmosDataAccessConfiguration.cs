namespace VideoPlatform.DAL.Infrastructure.Configurations;

public sealed class CosmosDataAccessConfiguration
{
    public string Account { get; set; }

    public string Key { get; set; }

    public string DatabaseName { get; set; }
}