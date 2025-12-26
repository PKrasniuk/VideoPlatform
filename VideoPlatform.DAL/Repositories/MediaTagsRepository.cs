using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class MediaTagsRepository(VideoPlatformContext dbContext)
    : EntityRepository<MediaTag, int>(dbContext), IMediaTagsRepository;