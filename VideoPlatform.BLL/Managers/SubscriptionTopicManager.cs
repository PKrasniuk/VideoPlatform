using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class SubscriptionTopicManager(ISubscriptionTopicsRepository subscriptionTopicsRepository)
    : ISubscriptionTopicManager
{
    private readonly ISubscriptionTopicsRepository _subscriptionTopicsRepository = subscriptionTopicsRepository ??
        throw new ArgumentNullException(nameof(subscriptionTopicsRepository));
}