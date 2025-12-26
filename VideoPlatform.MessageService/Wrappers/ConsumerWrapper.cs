using Confluent.Kafka;
using VideoPlatform.MessageService.Interfaces;
using VideoPlatform.MessageService.Models.Enums;

namespace VideoPlatform.MessageService.Wrappers;

public sealed class ConsumerWrapper(ConsumerConfig config) : IConsumerWrapper
{
    private readonly IConsumer<string, string> _consumer = new ConsumerBuilder<string, string>(config).Build();
    private bool _disposed;

    public void Subscribe(MessageType messageType)
    {
        _consumer?.Subscribe(messageType.ToString());
    }

    public string ReadMessage()
    {
        var consumeResult = _consumer.Consume();
        return consumeResult.Message.Value;
    }

    public void Close()
    {
        Dispose(true);
    }

    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
            _consumer?.Dispose();

        _disposed = true;
    }
}