using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class MediaRepository(VideoPlatformContext dbContext)
    : EntityRepository<Media, long>(dbContext), IMediaRepository;