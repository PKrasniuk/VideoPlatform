using System;
using System.Threading.Tasks;
using DotNetCore.CAP;
using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;
using VideoPlatform.MessageService.IntegrationEvents.Events;

namespace VideoPlatform.MessageService.IntegrationEvents.EventHandling
{
    public class PartnerTypesAddIntegrationEventHandler : ICapSubscribe
    {
        private readonly IPartnerTypesRepository _repository;

        public PartnerTypesAddIntegrationEventHandler(IPartnerTypesRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

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
}