using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingPlaylistManager(IElasticClient elasticClient)
    : IndexingEntityManager<Playlist>(elasticClient), IIndexingPlaylistManager;