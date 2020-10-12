using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers
{
    public class SubscriptionTopicManager : ISubscriptionTopicManager
    {
        private readonly ISubscriptionTopicsRepository _subscriptionTopicsRepository;

        public SubscriptionTopicManager(ISubscriptionTopicsRepository subscriptionTopicsRepository)
        {
            _subscriptionTopicsRepository = subscriptionTopicsRepository ?? throw new ArgumentNullException(nameof(subscriptionTopicsRepository));
        }
    }
}