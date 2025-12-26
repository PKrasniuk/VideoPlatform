using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingPlaylistMediaManager(IElasticClient elasticClient)
    : IndexingEntityManager<PlaylistMedia>(elasticClient), IIndexingPlaylistMediaManager;