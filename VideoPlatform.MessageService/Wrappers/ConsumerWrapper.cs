using Confluent.Kafka;
using VideoPlatform.MessageService.Interfaces;
using VideoPlatform.MessageService.Models.Enums;

namespace VideoPlatform.MessageService.Wrappers
{
    public class ConsumerWrapper : IConsumerWrapper
    {
        private readonly IConsumer<string, string> _consumer;

        public ConsumerWrapper(ConsumerConfig config)
        {
            _consumer = new ConsumerBuilder<string, string>(config).Build();
        }

        public void Subscribe(MessageType messageType)
        {
            _consumer.Subscribe(messageType.ToString());
        }

        public string ReadMessage()
        {
            var consumeResult = _consumer.Consume();
            return consumeResult.Message.Value;
        }

        public void Close()
        {
            _consumer.Close();
        }

        public void Dispose()
        {
            _consumer.Dispose();
        }
    }
}