using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories
{
    public class SubscriptionSeriesRepository : EntityRepository<SubscriptionSeries, int>, ISubscriptionSeriesRepository
    {
        public SubscriptionSeriesRepository(VideoPlatformContext dbContext) : base(dbContext)
        {
        }
    }
}