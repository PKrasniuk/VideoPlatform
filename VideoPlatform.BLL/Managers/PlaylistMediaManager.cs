using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class PlaylistMediaManager(IPlaylistMediaRepository playlistMediaRepository) : IPlaylistMediaManager
{
    private readonly IPlaylistMediaRepository _playlistMediaRepository =
        playlistMediaRepository ?? throw new ArgumentNullException(nameof(playlistMediaRepository));
}