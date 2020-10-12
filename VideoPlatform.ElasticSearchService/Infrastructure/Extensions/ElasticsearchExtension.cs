using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.ElasticSearchService.Infrastructure.Extensions
{
    public static partial class ConfigurationExtension
    {
        public static void AddElasticSearchConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var indexName = configuration["ElasticSearch:index"];

            var settings = new ConnectionSettings(new Uri(configuration["ElasticSearch:url"]))
                .DefaultIndex(indexName)
                .DisableDirectStreaming()
                .PrettyJson()
                .DefaultMappingFor<Partner>(m => m.IndexName($"{indexName}_{typeof(Partner).Name.ToLowerInvariant()}"))
                .DefaultMappingFor<PartnerTypes>(m => m.IndexName($"{indexName}_{typeof(PartnerTypes).Name.ToLowerInvariant()}"));

            var client = new ElasticClient(settings);

            client.Indices.Create($"{indexName}_{typeof(Partner).Name.ToLowerInvariant()}", m => m.Map<Partner>(x => x.AutoMap()));//TODO
            client.Indices.Create($"{indexName}_{typeof(PartnerTypes).Name.ToLowerInvariant()}", m => m.Map<PartnerTypes>(x => x.AutoMap()));

            services.AddSingleton<IElasticClient>(client);
        }
    }
}