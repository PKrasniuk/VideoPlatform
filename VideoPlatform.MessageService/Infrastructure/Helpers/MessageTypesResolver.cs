using System;
using VideoPlatform.MessageService.Models;
using VideoPlatform.MessageService.Models.Enums;

namespace VideoPlatform.MessageService.Infrastructure.Helpers
{
    public static class MessageTypesResolver
    {
        public static MessageParameters GetMessageParameters(MessageType type)
        {
            switch (type)
            {
                case MessageType.PartnerTypesAdd:
                    return new MessageParameters
                    {
                        ExchangeName = "videoplatform.exchange",
                        QueueName = "videoplatform.queue.partnertypesadd",
                        RouteKey = "videoplatform.queue.*"
                    };
                case MessageType.PartnerTypesRemove:
                    return new MessageParameters
                    {
                        ExchangeName = "videoplatform.exchange",
                        QueueName = "videoplatform.queue.partnertypesremove",
                        RouteKey = "videoplatform.queue.*"
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}