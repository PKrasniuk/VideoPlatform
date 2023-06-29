using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.ElasticSearchService.Infrastructure.Extensions;

public static class ConfigurationExtension
{
    public static void AddElasticSearchConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var indexName = configuration["ElasticSearch:index"];

        var settings = new ConnectionSettings(new Uri(configuration["ElasticSearch:url"]))
            .DefaultIndex(indexName)
            .DisableDirectStreaming()
            .PrettyJson()
            .DefaultMappingFor<Partner>(m => m.IndexName($"{indexName}_{nameof(Partner).ToLowerInvariant()}"))
            .DefaultMappingFor<PartnerTypes>(m =>
                m.IndexName($"{indexName}_{nameof(PartnerTypes).ToLowerInvariant()}"));

        var client = new ElasticClient(settings);

        client.Indices.Create($"{indexName}_{nameof(Partner).ToLowerInvariant()}",
            m => m.Map<Partner>(x => x.AutoMap()));
        client.Indices.Create($"{indexName}_{nameof(PartnerTypes).ToLowerInvariant()}",
            m => m.Map<PartnerTypes>(x => x.AutoMap()));

        services.AddSingleton<IElasticClient>(client);
    }
}