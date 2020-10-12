using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers
{
    public class PlaylistManager : IPlaylistManager
    {
        private readonly IPlaylistsRepository _playlistsRepository;

        public PlaylistManager(IPlaylistsRepository playlistsRepository)
        {
            _playlistsRepository = playlistsRepository ?? throw new ArgumentNullException(nameof(playlistsRepository));
        }
    }
}