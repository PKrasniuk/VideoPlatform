using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers
{
    public class PlaylistMediaManager : IPlaylistMediaManager
    {
        private readonly IPlaylistMediaRepository _playlistMediaRepository;

        public PlaylistMediaManager(IPlaylistMediaRepository playlistMediaRepository)
        {
            _playlistMediaRepository = playlistMediaRepository ?? throw new ArgumentNullException(nameof(playlistMediaRepository));
        }
    }
}