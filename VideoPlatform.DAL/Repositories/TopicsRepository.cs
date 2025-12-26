using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class TopicsRepository(VideoPlatformContext dbContext)
    : EntityRepository<Topic, int>(dbContext), ITopicsRepository;