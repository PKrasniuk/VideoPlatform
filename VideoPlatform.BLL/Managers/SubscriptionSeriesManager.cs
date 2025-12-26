using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class SubscriptionSeriesManager(ISubscriptionSeriesRepository subscriptionSeriesRepository)
    : ISubscriptionSeriesManager
{
    private readonly ISubscriptionSeriesRepository _subscriptionSeriesRepository = subscriptionSeriesRepository ??
        throw new ArgumentNullException(nameof(subscriptionSeriesRepository));
}