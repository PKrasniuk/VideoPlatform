using System;
using System.Text;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using RabbitMQ.Client;
using VideoPlatform.MessageService.Infrastructure.Helpers;
using VideoPlatform.MessageService.Interfaces;
using VideoPlatform.MessageService.Models.Enums;

namespace VideoPlatform.MessageService.Managers
{
    public class RabbitManager : IRabbitManager
    {
        private readonly DefaultObjectPool<IModel> _objectPool;

        public RabbitManager(IPooledObjectPolicy<IModel> objectPolicy)
        {
            _objectPool = new DefaultObjectPool<IModel>(objectPolicy, Environment.ProcessorCount * 2);
        }

        public void Publish<T>(T message, MessageType type) where T : class
        {
            if (message == null)
            {
                return;
            }

            var channel = _objectPool.Get();
            var parameters = MessageTypesResolver.GetMessageParameters(type);

            try
            {
                if (channel != null)
                {
                    channel.ExchangeDeclare(parameters.ExchangeName, ExchangeType.Topic, true, false, null);
                    channel.QueueDeclare(parameters.QueueName, true, false, false, null);
                    channel.QueueBind(parameters.QueueName, parameters.ExchangeName, parameters.RouteKey, null);
                    channel.BasicQos(0, 1, false);

                    var sendBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(parameters.ExchangeName, parameters.RouteKey, properties, sendBytes);
                }
            }
            finally
            {
                _objectPool.Return(channel);
            }
        }
    }
}