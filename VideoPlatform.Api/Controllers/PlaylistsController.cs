using System;
using Microsoft.AspNetCore.Mvc;
using VideoPlatform.BLL.Interfaces;

namespace VideoPlatform.Api.Controllers
{
    /// <summary>
    /// PlaylistsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistManager _playlistManager;

        /// <summary>
        /// PlaylistsController constructor
        /// </summary>
        /// <param name="playlistManager"></param>
        public PlaylistsController(IPlaylistManager playlistManager)
        {
            _playlistManager = playlistManager ?? throw new ArgumentNullException(nameof(playlistManager));
        }
    }
}