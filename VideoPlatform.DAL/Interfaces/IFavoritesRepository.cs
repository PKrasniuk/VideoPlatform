using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Interfaces;

public interface IFavoritesRepository : IEntityRepository<Favorite, int>
{
}