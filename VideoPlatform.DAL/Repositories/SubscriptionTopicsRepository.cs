using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class SubscriptionTopicsRepository(VideoPlatformContext dbContext)
    : EntityRepository<SubscriptionTopic, int>(dbContext), ISubscriptionTopicsRepository;