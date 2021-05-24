using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
using VideoPlatform.MessageService.Infrastructure.Configuration;
using VideoPlatform.MessageService.IntegrationEvents.EventHandling;
using VideoPlatform.MessageService.Interfaces;
using VideoPlatform.MessageService.Managers;
using VideoPlatform.MessageService.Models;
using VideoPlatform.MessageService.Wrappers;

namespace VideoPlatform.MessageService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMessenger(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitOptions>(options =>
            {
                options.UserName = configuration["RabbitMQ:UserName"];
                options.Password = configuration["RabbitMQ:Password"];
                options.HostName = configuration["RabbitMQ:HostName"];
                options.VHost = configuration["RabbitMQ:VHost"];
                options.Port = int.Parse(configuration["RabbitMQ:Port"]);
            });

            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton<IPooledObjectPolicy<IModel>, RabbitModelPooledObjectPolicy>();

            services.AddSingleton<IRabbitManager, RabbitManager>();
            services.AddHostedService<PartnerTypesRemoveListener>();

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCap(options =>
            {
                //options.UseDashboard();
                options.UseSqlServer(configuration["ConnectionStrings:Default"]);

                options.UseRabbitMQ(conf =>
                {
                    conf.HostName = configuration["RabbitMQ:HostName"];
                    conf.Port = int.Parse(configuration["RabbitMQ:Port"]);
                    conf.UserName = configuration["RabbitMQ:UserName"];
                    conf.Password = configuration["RabbitMQ:Password"];
                    conf.VirtualHost = configuration["RabbitMQ:VHost"];
                });

                options.FailedRetryCount = int.Parse(configuration["RabbitMQ:MessengerRetryCount"]);
                //options.DefaultGroup = configuration["RabbitMQ:MessengerClientName"];
            });

            return services;
        }

        public static IServiceCollection RegisteringEventHandlers(this IServiceCollection services)
        {
            services.AddTransient<PartnerTypesAddIntegrationEventHandler>();
            services.AddTransient<PartnerTypesRemoveIntegrationEventHandler>();

            return services;
        }

        public static IServiceCollection AddKafkaMessenger(this IServiceCollection services, IConfiguration configuration)
        {
            var producerConfig = new ProducerConfig();
            configuration.Bind("Kafka:Producer", producerConfig);
            services.AddSingleton(producerConfig);

            var consumerConfig = new ConsumerConfig();
            configuration.Bind("Kafka:Consumer", consumerConfig);
            services.AddSingleton(consumerConfig);

            services.AddTransient<IProducerWrapper, ProducerWrapper>();
            services.AddTransient<IConsumerWrapper, ConsumerWrapper>();

            services.AddHostedService<PartnerTypesRemoveKafkaListener>();

            return services;
        }
    }
}