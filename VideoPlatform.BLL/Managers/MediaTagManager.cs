using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers
{
    public class MediaTagManager : IMediaTagManager
    {
        private readonly IMediaTagsRepository _mediaTagsRepository;

        public MediaTagManager(IMediaTagsRepository mediaTagsRepository)
        {
            _mediaTagsRepository = mediaTagsRepository ?? throw new ArgumentNullException(nameof(mediaTagsRepository));
        }
    }
}