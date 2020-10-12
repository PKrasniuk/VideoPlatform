using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories
{
    public class FavoritesRepository : EntityRepository<Favorite, int>, IFavoritesRepository
    {
        public FavoritesRepository(VideoPlatformContext dbContext) : base(dbContext)
        {
        }
    }
}