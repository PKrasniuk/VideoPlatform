using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.BLL.Managers
{
    public class InfoDataManager : IInfoDataManager
    {
        private readonly IInfoDataRepository _infoDataRepository;

        public InfoDataManager(IInfoDataRepository infoDataRepository)
        {
            _infoDataRepository = infoDataRepository ?? throw new ArgumentNullException(nameof(infoDataRepository));
        }

        public async Task<InfoData> GetInfoDataByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _infoDataRepository.GetEntityByIdAsync(id, cancellationToken);
        }

        public async Task<InfoData> GetInfoDataByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _infoDataRepository.GetEntityAsync(x => x.Name.Equals(name), cancellationToken);
        }

        public async Task<ICollection<InfoData>> GetInfoDataAsync(CancellationToken cancellationToken)
        {
            return await _infoDataRepository.GetEntitiesAsync(null, cancellationToken);
        }

        public async Task<PagingResult<InfoData>> GetInfoDataAsync(Paging<InfoData> filter, CancellationToken cancellationToken)
        {
            return await _infoDataRepository.GetPagingEntitiesAsync(filter, cancellationToken);
        }

        public async Task<InfoData> SaveInfoDataAsync(InfoData entity, CancellationToken cancellationToken)
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
                return await _infoDataRepository.CreateEntityAsync(entity, cancellationToken);
            }

            await _infoDataRepository.UpdateEntityAsync(entity, cancellationToken);

            return entity;
        }

        public async Task<IList<InfoData>> AddInfoDataAsync(IList<InfoData> entities, CancellationToken cancellationToken)
        {
            return await _infoDataRepository.CreateEntitiesAsync(entities, cancellationToken);
        }

        public async Task UpdateInfoDataAsync(IList<InfoData> entities, CancellationToken cancellationToken)
        {
            await _infoDataRepository.UpdateEntitiesAsync(entities, cancellationToken);
        }

        public async Task RemoveInfoDataAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _infoDataRepository.GetEntityByIdAsync(id, cancellationToken);
            if (entity != null) 
                await _infoDataRepository.RemoveEntityAsync(id, cancellationToken);
        }

        public async Task RemoveInfoDataAsync(IList<Guid> ids, CancellationToken cancellationToken)
        {
            await _infoDataRepository.RemoveEntitiesAsync(ids, cancellationToken);
        }
    }
}