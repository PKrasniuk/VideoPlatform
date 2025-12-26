using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class FavoriteManager(IFavoritesRepository favoritesRepository) : IFavoriteManager
{
    private readonly IFavoritesRepository _favoritesRepository =
        favoritesRepository ?? throw new ArgumentNullException(nameof(favoritesRepository));
}