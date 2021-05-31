using System;
using System.Threading.Tasks;
using DotNetCore.CAP;
using VideoPlatform.DAL.Interfaces;
using VideoPlatform.MessageService.IntegrationEvents.Events;

namespace VideoPlatform.MessageService.IntegrationEvents.EventHandling
{
    public class PartnerTypesRemoveIntegrationEventHandler : ICapSubscribe
    {
        private readonly IPartnerTypesRepository _repository;

        public PartnerTypesRemoveIntegrationEventHandler(IPartnerTypesRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [CapSubscribe(nameof(PartnerTypesRemoveIntegrationEvent))]
        public async Task HandleAsync(PartnerTypesRemoveIntegrationEvent @event)
        {
            var partnerTypes =
                await _repository.GetEntityAsync(x => x.PartnerId == @event.PartnerId && x.Type.Equals(@event.Type));
            if (partnerTypes != null) 
                await _repository.RemoveEntityAsync(partnerTypes.Id);
        }
    }
}