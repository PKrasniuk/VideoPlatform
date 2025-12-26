using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class SubscriptionSeriesRepository(VideoPlatformContext dbContext)
    : EntityRepository<SubscriptionSeries, int>(dbContext), ISubscriptionSeriesRepository;