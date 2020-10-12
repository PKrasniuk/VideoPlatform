using System;
using Microsoft.AspNetCore.Mvc;
using VideoPlatform.BLL.Interfaces;

namespace VideoPlatform.Api.Controllers
{
    /// <summary>
    /// SubscriptionSeriesController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionSeriesController : ControllerBase
    {
        private readonly ISubscriptionSeriesManager _subscriptionSeriesManager;

        /// <summary>
        /// SubscriptionSeriesController constructor
        /// </summary>
        /// <param name="subscriptionSeriesManager"></param>
        public SubscriptionSeriesController(ISubscriptionSeriesManager subscriptionSeriesManager)
        {
            _subscriptionSeriesManager = subscriptionSeriesManager ?? throw new ArgumentNullException(nameof(subscriptionSeriesManager));
        }
    }
}