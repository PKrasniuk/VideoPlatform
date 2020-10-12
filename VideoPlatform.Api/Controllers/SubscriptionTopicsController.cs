using System;
using Microsoft.AspNetCore.Mvc;
using VideoPlatform.BLL.Interfaces;

namespace VideoPlatform.Api.Controllers
{
    /// <summary>
    /// SubscriptionTopicsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionTopicsController : ControllerBase
    {
        private readonly ISubscriptionTopicManager _subscriptionTopicManager;

        /// <summary>
        /// SubscriptionTopicsController constructor
        /// </summary>
        /// <param name="subscriptionTopicManager"></param>
        public SubscriptionTopicsController(ISubscriptionTopicManager subscriptionTopicManager)
        {
            _subscriptionTopicManager = subscriptionTopicManager ?? throw new ArgumentNullException(nameof(subscriptionTopicManager));
        }
    }
}