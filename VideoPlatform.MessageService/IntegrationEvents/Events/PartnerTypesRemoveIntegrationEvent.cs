using Newtonsoft.Json;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.MessageService.IntegrationEvents.Events
{
    public class PartnerTypesRemoveIntegrationEvent
    {
        [JsonProperty("partnerId")]
        public int PartnerId { get; }

        [JsonProperty("partnerType")]
        public PartnerType Type { get; }

        public PartnerTypesRemoveIntegrationEvent(int partnerId, PartnerType type)
        {
            PartnerId = partnerId;
            Type = type;
        }
    }
}