﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.MessageService.Interfaces;
using VideoPlatform.MessageService.Models.Enums;

namespace VideoPlatform.MessageService.Managers;

public abstract class KafkaListener : BackgroundService
{
    private readonly IConsumerWrapper _consumerWrapper;
    private bool _disposed;

    protected KafkaListener(IConsumerWrapper consumerWrapper)
    {
        _consumerWrapper = consumerWrapper;
    }

    protected MessageType MessageType { private get; set; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var currentNumAttempts = 0;
            while (currentNumAttempts < ConfigurationConstants.MaxNumAttempts)
            {
                currentNumAttempts++;
                _consumerWrapper.Subscribe(MessageType);
                await ProcessAsync(_consumerWrapper.ReadMessage());
            }
        }
    }

    protected virtual Task ProcessAsync(string message)
    {
        throw new NotImplementedException();
    }

    public override void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            base.Dispose();
            _consumerWrapper.Dispose();
        }

        _disposed = true;
    }
}