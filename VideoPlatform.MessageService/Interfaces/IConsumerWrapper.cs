using VideoPlatform.MessageService.Models.Enums;

namespace VideoPlatform.MessageService.Interfaces;

public interface IConsumerWrapper
{
    void Subscribe(MessageType messageType);

    string ReadMessage();

    void Close();

    void Dispose();
}