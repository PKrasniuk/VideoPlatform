using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class PlaylistsRepository(VideoPlatformContext dbContext)
    : EntityRepository<Playlist, int>(dbContext), IPlaylistsRepository;