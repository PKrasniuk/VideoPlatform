using System.Threading.Tasks;
using VideoPlatform.MessageService.IntegrationEvents.Events;

namespace VideoPlatform.NotificationService.Interfaces
{
    /// <summary>
    /// ISubscriberService
    /// </summary>
    public interface ISubscriberManager
    {
        /// <summary>
        /// CheckPartnerTypesRemoveMessage
        /// </summary>
        /// <param name="event"></param>
        Task CheckPartnerTypesRemoveMessageAsync(PartnerTypesRemoveIntegrationEvent @event);
    }
}