using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class FavoritesRepository(VideoPlatformContext dbContext)
    : EntityRepository<Favorite, int>(dbContext), IFavoritesRepository;