using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.MessageService.Models
{
    public class PartnerTypesRemoveModel
    {
        public int PartnerId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PartnerType Type { get; set; }
    }
}