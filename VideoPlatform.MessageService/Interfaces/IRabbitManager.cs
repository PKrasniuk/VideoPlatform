using VideoPlatform.MessageService.Models.Enums;

namespace VideoPlatform.MessageService.Interfaces
{
    public interface IRabbitManager
    {
        void Publish<T>(T message, MessageType type) where T : class;
    }
}