using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class PlaylistManager(IPlaylistsRepository playlistsRepository) : IPlaylistManager
{
    private readonly IPlaylistsRepository _playlistsRepository =
        playlistsRepository ?? throw new ArgumentNullException(nameof(playlistsRepository));
}