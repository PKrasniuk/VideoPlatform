using Newtonsoft.Json;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.MessageService.IntegrationEvents.Events;

public class PartnerTypesRemoveIntegrationEvent(int partnerId, PartnerType type)
{
    [JsonProperty("partnerId")] public int PartnerId { get; } = partnerId;

    [JsonProperty("partnerType")] public PartnerType Type { get; } = type;
}