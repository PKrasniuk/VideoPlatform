using VideoPlatform.Domain.Enums;

namespace VideoPlatform.MessageService.IntegrationEvents.Events;

public class PartnerTypesAddIntegrationEvent
{
    public PartnerTypesAddIntegrationEvent(int partnerId, PartnerType type)
    {
        PartnerId = partnerId;
        Type = type;
    }

    public int PartnerId { get; }

    public PartnerType Type { get; }
}