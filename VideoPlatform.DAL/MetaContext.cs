using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VideoPlatform.DAL.Infrastructure.Configurations;

namespace VideoPlatform.DAL
{
    public class MetaContext
    {
        public MetaContext(IOptions<MetaDataAccessConfiguration> options)
        {
            Client = new MongoClient(options.Value.ConnectionString);
            MetaDatabase = Client.GetDatabase(options.Value.DatabaseName);
        }

        public MongoClient Client { get; }

        public IMongoDatabase MetaDatabase { get; }
    }
}