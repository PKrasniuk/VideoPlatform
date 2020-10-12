using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Newtonsoft.Json;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.BLL.Models;
using VideoPlatform.DAL.Infrastructure.Helpers;
using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;
using VideoPlatform.Domain.Enums;
using VideoPlatform.ElasticSearchService.Interfaces;
using VideoPlatform.MessageService.IntegrationEvents.Events;
using VideoPlatform.MessageService.Interfaces;
using VideoPlatform.MessageService.Models;
using VideoPlatform.MessageService.Models.Enums;

namespace VideoPlatform.BLL.Managers
{
    public class PartnerTypesManager : IPartnerTypesManager
    {
        private readonly IPartnerTypesRepository _partnerTypesRepository;
        private readonly IIndexingPartnerTypesManager _indexingPartnerTypesManager;
        private readonly IRabbitManager _rabbitManager;
        private readonly ICapPublisher _eventBus;
        private readonly IProducerWrapper _producerWrapper;

        public PartnerTypesManager(IPartnerTypesRepository partnerTypesRepository,
            IIndexingPartnerTypesManager indexingPartnerTypesManager, IRabbitManager rabbitManager,
            ICapPublisher eventBus, IProducerWrapper producerWrapper)
        {
            _partnerTypesRepository =
                partnerTypesRepository ?? throw new ArgumentNullException(nameof(partnerTypesRepository));
            _indexingPartnerTypesManager = indexingPartnerTypesManager ??
                                           throw new ArgumentNullException(nameof(indexingPartnerTypesManager));
            _rabbitManager = rabbitManager ?? throw new ArgumentNullException(nameof(rabbitManager));
            _producerWrapper = producerWrapper ?? throw new ArgumentNullException(nameof(producerWrapper));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public ICollection<ListItem<byte>> GetPartnerTypes(CancellationToken cancellationToken)
        {
            var result = new Collection<ListItem<byte>>();

            foreach (var partnerType in (PartnerType[])Enum.GetValues(typeof(PartnerType)))
            {
                result.Add(new ListItem<byte>
                {
                    Id = (byte) partnerType,
                    Name = partnerType.GetDescription()
                });
            }

            return result;
        }

        public async Task<ICollection<ListItem<byte>>> GetPartnerTypesByPartnerIdAsync(int partnerId, CancellationToken cancellationToken)
        {
            var partnerTypes = await _partnerTypesRepository.GetEntitiesAsync(x => x.PartnerId == partnerId, cancellationToken);
            if (partnerTypes != null && partnerTypes.Any())
            {
                var result = new Collection<ListItem<byte>>();

                foreach (var partnerType in partnerTypes)
                {
                    result.Add(new ListItem<byte>
                    {
                        Id = (byte) partnerType.Type,
                        Name = partnerType.Type.GetDescription()
                    });
                }

                return result;
            }

            return null;
        }

        public async Task<ICollection<PartnerTypes>> GetPartnerTypesCollectionAsync(CancellationToken cancellationToken)
        {
            return await _partnerTypesRepository.GetEntitiesAsync(null, cancellationToken);
        }

        public async Task<ICollection<PartnerTypes>> GetPartnerTypesCollectionAsync(int partnerId, CancellationToken cancellationToken)
        {
            var partnerTypes = await _partnerTypesRepository.GetEntitiesAsync(x => x.PartnerId == partnerId, cancellationToken);
            return partnerTypes != null && partnerTypes.Any() ? partnerTypes : null;
        }

        public async Task<PartnerTypes> AddPartnerTypeAsync(int partnerId, PartnerType type, CancellationToken cancellationToken)
        {
            var dbPartnerType =
                await _partnerTypesRepository.GetEntityAsync(x => x.PartnerId == partnerId && x.Type == type, cancellationToken);
            if (dbPartnerType == null)
            {
                var result = await _partnerTypesRepository.CreateEntityAsync(new PartnerTypes
                {
                    PartnerId = partnerId,
                    Type = type
                }, cancellationToken);

                await _indexingPartnerTypesManager.IndexEntityAsync(result, cancellationToken);

                return result;
            }

            return dbPartnerType;
        }

        public async Task RemovePartnerTypeAsync(int partnerId, PartnerType type, CancellationToken cancellationToken)
        {
            var dbPartnerType =
                await _partnerTypesRepository.GetEntityAsync(x => x.PartnerId == partnerId && x.Type == type,
                    cancellationToken);
            if (dbPartnerType != null)
            {
                await _partnerTypesRepository.RemoveEntityAsync(dbPartnerType.Id, cancellationToken);

                await _indexingPartnerTypesManager.RemoveEntityAsync(dbPartnerType, cancellationToken);
            }
        }

        public async Task RemovePartnerTypeByEventBusAsync(int partnerId, PartnerType type, CancellationToken cancellationToken)
        {
            var dbPartnerType =
                await _partnerTypesRepository.GetEntityAsync(x => x.PartnerId == partnerId && x.Type == type,
                    cancellationToken);
            if (dbPartnerType != null)
            {
                await _eventBus.PublishAsync(nameof(PartnerTypesRemoveIntegrationEvent),
                    new PartnerTypesRemoveIntegrationEvent(dbPartnerType.PartnerId, dbPartnerType.Type), null,
                    cancellationToken);

                await _indexingPartnerTypesManager.RemoveEntityAsync(dbPartnerType, cancellationToken);
            }
        }

        public async Task RemovePartnerTypeByRabbitMQAsync(int partnerId, PartnerType type, CancellationToken cancellationToken)
        {
            var dbPartnerType =
                await _partnerTypesRepository.GetEntityAsync(x => x.PartnerId == partnerId && x.Type == type,
                    cancellationToken);
            if (dbPartnerType != null)
            {
                _rabbitManager.Publish(JsonConvert.SerializeObject(new PartnerTypesRemoveModel
                {
                    PartnerId = dbPartnerType.PartnerId,
                    Type = dbPartnerType.Type
                }), MessageType.PartnerTypesRemove);

                await _indexingPartnerTypesManager.RemoveEntityAsync(dbPartnerType, cancellationToken);
            }
        }

        public async Task RemovePartnerTypeByKafkaAsync(int partnerId, PartnerType type, CancellationToken cancellationToken)
        {
            var dbPartnerType =
                await _partnerTypesRepository.GetEntityAsync(x => x.PartnerId == partnerId && x.Type == type,
                    cancellationToken);
            if (dbPartnerType != null)
            {
                await _producerWrapper.WriteMessage(JsonConvert.SerializeObject(new PartnerTypesRemoveModel
                {
                    PartnerId = dbPartnerType.PartnerId,
                    Type = dbPartnerType.Type
                }), MessageType.PartnerTypesRemove, cancellationToken);

                await _indexingPartnerTypesManager.RemoveEntityAsync(dbPartnerType, cancellationToken);
            }
        }

        public async Task ReIndexingPartnerTypesAsync(CancellationToken cancellationToken)
        {
            var partnerTypes = await _partnerTypesRepository.GetEntitiesAsync(null, cancellationToken);
            if (partnerTypes != null && partnerTypes.Any())
            {
                await _indexingPartnerTypesManager.ReIndex(partnerTypes, cancellationToken);
            }
        }
    }
}