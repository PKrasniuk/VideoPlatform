using VideoPlatform.Domain.Enums;

namespace VideoPlatform.MessageService.IntegrationEvents.Events
{
    public class PartnerTypesAddIntegrationEvent
    {
        public int PartnerId { get; }

        public PartnerType Type { get; }

        public PartnerTypesAddIntegrationEvent(int partnerId, PartnerType type)
        {
            PartnerId = partnerId;
            Type = type;
        }
    }
}