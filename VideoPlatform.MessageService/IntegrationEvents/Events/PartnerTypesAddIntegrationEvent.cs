using VideoPlatform.Domain.Enums;

namespace VideoPlatform.MessageService.IntegrationEvents.Events;

public class PartnerTypesAddIntegrationEvent(int partnerId, PartnerType type)
{
    public int PartnerId { get; } = partnerId;

    public PartnerType Type { get; } = type;
}