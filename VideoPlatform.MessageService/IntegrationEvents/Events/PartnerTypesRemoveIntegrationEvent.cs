using Newtonsoft.Json;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.MessageService.IntegrationEvents.Events;

public class PartnerTypesRemoveIntegrationEvent
{
    public PartnerTypesRemoveIntegrationEvent(int partnerId, PartnerType type)
    {
        PartnerId = partnerId;
        Type = type;
    }

    [JsonProperty("partnerId")] public int PartnerId { get; }

    [JsonProperty("partnerType")] public PartnerType Type { get; }
}