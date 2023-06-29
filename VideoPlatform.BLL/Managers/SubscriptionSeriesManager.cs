using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class SubscriptionSeriesManager : ISubscriptionSeriesManager
{
    private readonly ISubscriptionSeriesRepository _subscriptionSeriesRepository;

    public SubscriptionSeriesManager(ISubscriptionSeriesRepository subscriptionSeriesRepository)
    {
        _subscriptionSeriesRepository = subscriptionSeriesRepository ??
                                        throw new ArgumentNullException(nameof(subscriptionSeriesRepository));
    }
}