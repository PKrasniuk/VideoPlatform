using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class PlaylistMediaRepository(VideoPlatformContext dbContext)
    : EntityRepository<PlaylistMedia, int>(dbContext), IPlaylistMediaRepository;