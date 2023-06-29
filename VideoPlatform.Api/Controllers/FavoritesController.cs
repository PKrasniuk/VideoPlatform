using System;
using Microsoft.AspNetCore.Mvc;
using VideoPlatform.BLL.Interfaces;

namespace VideoPlatform.Api.Controllers;

/// <summary>
///     FavoritesController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class FavoritesController : ControllerBase
{
    private readonly IFavoriteManager _favoriteManager;

    /// <summary>
    ///     FavoritesController constructor
    /// </summary>
    /// <param name="favoriteManager"></param>
    public FavoritesController(IFavoriteManager favoriteManager)
    {
        _favoriteManager = favoriteManager ?? throw new ArgumentNullException(nameof(favoriteManager));
    }
}