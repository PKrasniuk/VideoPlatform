using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using VideoPlatform.MessageService.Infrastructure.Helpers;
using VideoPlatform.MessageService.Models;
using VideoPlatform.MessageService.Models.Enums;

namespace VideoPlatform.MessageService.Managers
{
    public abstract class RabbitListener : IHostedService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly RabbitOptions _options;

        protected RabbitListener(IOptions<RabbitOptions> optionsAccess)
        {
            _options = optionsAccess.Value;
            if (_options != null)
            {
                _connection = GetConnection();
                if (_connection != null)
                {
                    _channel = _connection.CreateModel();
                }
            }
        }

        private IConnection GetConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _options.HostName,
                    UserName = _options.UserName,
                    Password = _options.Password,
                    Port = _options.Port,
                    VirtualHost = _options.VHost,
                };

                return factory.CreateConnection();
            }
            catch
            {
                return null;
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Register();

            return Task.CompletedTask;
        }

        protected MessageType MessageType { private get; set; }

        protected virtual bool Process(string message)
        {
            throw new NotImplementedException();
        }

        private void Register()
        {
            var parameters = MessageTypesResolver.GetMessageParameters(MessageType);

            if (_connection != null)
            {
                //var retryHandle = Policy.Handle<Exception>()
                //    .WaitAndRetry(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
                //retryHandle.Execute(() =>
                //{
                //});

                _channel.ExchangeDeclare(parameters.ExchangeName, ExchangeType.Topic, true, false, null);
                _channel.QueueDeclare(parameters.QueueName, true, false, false, null);
                _channel.QueueBind(parameters.QueueName, parameters.ExchangeName, parameters.RouteKey, null);
                _channel.BasicQos(0, 1, false);

                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(body));
                    var result = Process(message.ToString());
                    if (result)
                    {
                        _channel.BasicAck(ea.DeliveryTag, false);
                    }
                };

                _channel.BasicConsume(queue: parameters.QueueName, autoAck: false, consumer: consumer);
            }
        }
        
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _connection?.Close();

            return Task.CompletedTask;
        }
    }
}