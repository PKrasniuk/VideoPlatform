using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.MessageService.Interfaces;
using VideoPlatform.MessageService.Models.Enums;

namespace VideoPlatform.MessageService.Managers;

public abstract class KafkaListener(IConsumerWrapper consumerWrapper) : BackgroundService
{
    private bool _disposed;

    protected MessageType MessageType { private get; set; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var currentNumAttempts = 0;
            while (currentNumAttempts < ConfigurationConstants.MaxNumAttempts)
            {
                currentNumAttempts++;
                consumerWrapper.Subscribe(MessageType);
                await ProcessAsync(consumerWrapper.ReadMessage());
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
            consumerWrapper.Dispose();
        }

        _disposed = true;
    }
}