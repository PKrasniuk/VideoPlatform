using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class TopicsRepository : EntityRepository<Topic, int>, ITopicsRepository
{
    public TopicsRepository(VideoPlatformContext dbContext) : base(dbContext)
    {
    }
}