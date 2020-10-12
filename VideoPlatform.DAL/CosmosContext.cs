using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Options;
using VideoPlatform.Common.Infrastructure.Helpers;
using VideoPlatform.DAL.Infrastructure.Configurations;

namespace VideoPlatform.DAL
{
    public class CosmosContext
    {
        public CosmosContext(IOptions<CosmosDataAccessConfiguration> options)
        {
            var clientBuilder = new CosmosClientBuilder(options.Value.Account, options.Value.Key);
            var client = clientBuilder.WithConnectionModeDirect().Build();
            CosmosDatabaseResponse = AsyncHelper.RunSync(async () =>
                await client.CreateDatabaseIfNotExistsAsync(options.Value.DatabaseName));
        }

        public DatabaseResponse CosmosDatabaseResponse { get; }
    }
}