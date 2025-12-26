using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class MediaManager(IMediaRepository mediaRepository) : IMediaManager
{
    private readonly IMediaRepository _mediaRepository =
        mediaRepository ?? throw new ArgumentNullException(nameof(mediaRepository));
}