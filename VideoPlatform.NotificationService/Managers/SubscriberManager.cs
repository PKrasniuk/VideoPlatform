using System;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.MessageService.IntegrationEvents.Events;
using VideoPlatform.NotificationService.Hubs;
using VideoPlatform.NotificationService.Interfaces;

namespace VideoPlatform.NotificationService.Managers
{
    /// <summary>
    /// SubscriberManager
    /// </summary>
    public class SubscriberManager : ISubscriberManager, ICapSubscribe
    {
        private readonly IHubContext<NotificationHub> _hub;

        /// <summary>
        /// SubscriberManager constructor
        /// </summary>
        /// <param name="hub"></param>
        public SubscriberManager(IHubContext<NotificationHub> hub)
        {
            _hub = hub ?? throw new ArgumentNullException(nameof(hub));
        }

        /// <summary>
        /// CheckPartnerTypesRemoveMessageAsync
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        [CapSubscribe(nameof(PartnerTypesRemoveIntegrationEvent), Group = ConfigurationConstants.NotificationGroupName)]
        public async Task CheckPartnerTypesRemoveMessageAsync(PartnerTypesRemoveIntegrationEvent @event)
        {
            await _hub.Clients.All.SendAsync(nameof(PartnerTypesRemoveIntegrationEvent),
                JsonConvert.SerializeObject(@event));
        }
    }
}