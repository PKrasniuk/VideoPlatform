using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers
{
    public class FavoriteManager : IFavoriteManager
    {
        private readonly IFavoritesRepository _favoritesRepository;

        public FavoriteManager(IFavoritesRepository favoritesRepository)
        {
            _favoritesRepository = favoritesRepository ?? throw new ArgumentNullException(nameof(favoritesRepository));
        }
    }
}