﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using VideoPlatform.MessageService.Interfaces;
using VideoPlatform.MessageService.Models.Enums;

namespace VideoPlatform.MessageService.Wrappers;

public class ProducerWrapper : IProducerWrapper
{
    private static readonly Random Rand = new();
    private readonly ProducerConfig _config;

    public ProducerWrapper(ProducerConfig config)
    {
        _config = config;
    }

    public async Task<DeliveryResult<string, string>> WriteMessage(string message, MessageType messageType,
        CancellationToken cancellationToken)
    {
        using var producer = new ProducerBuilder<string, string>(_config).Build();
        return await producer.ProduceAsync(messageType.ToString(), new Message<string, string>
        {
            Key = Rand.Next(5).ToString(),
            Value = message
        }, cancellationToken);
    }
}