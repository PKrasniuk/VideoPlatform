using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class MediaTagManager(IMediaTagsRepository mediaTagsRepository) : IMediaTagManager
{
    private readonly IMediaTagsRepository _mediaTagsRepository =
        mediaTagsRepository ?? throw new ArgumentNullException(nameof(mediaTagsRepository));
}