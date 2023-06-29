using VideoPlatform.Domain.Entities;

namespace VideoPlatform.ElasticSearchService.Interfaces;

public interface IIndexingFavoriteManager : IIndexingEntityManager<Favorite>
{
}