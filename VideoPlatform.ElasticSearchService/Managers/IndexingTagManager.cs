using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingTagManager(IElasticClient elasticClient)
    : IndexingEntityManager<Tag>(elasticClient), IIndexingTagManager;