using Newtonsoft.Json;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.MessageService.IntegrationEvents.Events
{
    public class PartnerTypesRemoveIntegrationEvent
    {
        [JsonProperty(propertyName: "partnerId")]
        public int PartnerId { get; }

        [JsonProperty(propertyName: "partnerType")]
        public PartnerType Type { get; }

        public PartnerTypesRemoveIntegrationEvent(int partnerId, PartnerType type)
        {
            PartnerId = partnerId;
            Type = type;
        }
    }
}