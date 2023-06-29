using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class SubscriptionTopicsRepository : EntityRepository<SubscriptionTopic, int>, ISubscriptionTopicsRepository
{
    public SubscriptionTopicsRepository(VideoPlatformContext dbContext) : base(dbContext)
    {
    }
}