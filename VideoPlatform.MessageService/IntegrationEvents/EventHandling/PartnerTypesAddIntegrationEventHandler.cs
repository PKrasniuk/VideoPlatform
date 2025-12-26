using System;
using System.Threading.Tasks;
using DotNetCore.CAP;
using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;
using VideoPlatform.MessageService.IntegrationEvents.Events;

namespace VideoPlatform.MessageService.IntegrationEvents.EventHandling;

public class PartnerTypesAddIntegrationEventHandler(IPartnerTypesRepository repository) : ICapSubscribe
{
    private readonly IPartnerTypesRepository _repository =
        repository ?? throw new ArgumentNullException(nameof(repository));

    [CapSubscribe(nameof(PartnerTypesAddIntegrationEvent))]
    public async Task HandleAsync(PartnerTypesAddIntegrationEvent @event)
    {
        await _repository.CreateEntityAsync(new PartnerTypes
        {
            PartnerId = @event.PartnerId,
            Type = @event.Type
        });
    }
}