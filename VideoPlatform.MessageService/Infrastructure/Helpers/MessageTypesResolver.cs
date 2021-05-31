using System;
using VideoPlatform.MessageService.Models;
using VideoPlatform.MessageService.Models.Enums;

namespace VideoPlatform.MessageService.Infrastructure.Helpers
{
    public static class MessageTypesResolver
    {
        public static MessageParameters GetMessageParameters(MessageType type)
        {
            return type switch
            {
                MessageType.PartnerTypesAdd => new MessageParameters
                {
                    ExchangeName = "videoplatform.exchange",
                    QueueName = "videoplatform.queue.partnertypesadd",
                    RouteKey = "videoplatform.queue.*"
                },
                MessageType.PartnerTypesRemove => new MessageParameters
                {
                    ExchangeName = "videoplatform.exchange",
                    QueueName = "videoplatform.queue.partnertypesremove",
                    RouteKey = "videoplatform.queue.*"
                },
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}