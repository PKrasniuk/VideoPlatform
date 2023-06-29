using System;
using Microsoft.AspNetCore.Mvc;
using VideoPlatform.BLL.Interfaces;

namespace VideoPlatform.Api.Controllers;

/// <summary>
///     PlaylistMediaController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PlaylistMediaController : ControllerBase
{
    private readonly IPlaylistMediaManager _playlistMediaManager;

    /// <summary>
    ///     PlaylistMediaController constructor
    /// </summary>
    /// <param name="playlistMediaManager"></param>
    public PlaylistMediaController(IPlaylistMediaManager playlistMediaManager)
    {
        _playlistMediaManager = playlistMediaManager ?? throw new ArgumentNullException(nameof(playlistMediaManager));
    }
}