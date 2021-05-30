using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.BLL.Managers
{
    public class MetaDataManager : IMetaDataManager
    {
        private readonly IMetaDataRepository _metaDataRepository;

        public MetaDataManager(IMetaDataRepository metaDataRepository)
        {
            _metaDataRepository = metaDataRepository ?? throw new ArgumentNullException(nameof(metaDataRepository));
        }

        public async Task<MetaData> GetMetaDataByIdAsync(ObjectId id, CancellationToken cancellationToken)
        {
            return await _metaDataRepository.GetMetaEntityByIdAsync(id, cancellationToken);
        }

        public async Task<MetaData> GetMetaDataByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _metaDataRepository.GetMetaEntityAsync(x => x.Name.Equals(name), cancellationToken);
        }

        public async Task<ICollection<MetaData>> GetMetaDataAsync(CancellationToken cancellationToken)
        {
            return await _metaDataRepository.GetMetaEntitiesAsync(null, cancellationToken);
        }

        public async Task<PagingResult<MetaData>> GetMetaDataAsync(MetaPaging<MetaData> filter, CancellationToken cancellationToken)
        {
            return await _metaDataRepository.GetPagingMetaEntitiesAsync(filter, cancellationToken);
        }

        public async Task<MetaData> SaveMetaDataAsync(MetaData entity, CancellationToken cancellationToken)
        {
            if (entity.Id == ObjectId.Empty)
                return await _metaDataRepository.CreateMetaEntityAsync(entity, cancellationToken);

            var updateStatus = await _metaDataRepository.UpdateMetaEntityAsync(entity, cancellationToken);

            return updateStatus ? entity : null;
        }

        public async Task<IList<MetaData>> AddMetaDataAsync(IList<MetaData> entities, CancellationToken cancellationToken)
        {
            return await _metaDataRepository.CreateMetaEntitiesAsync(entities, cancellationToken);
        }

        public async Task UpdateMetaDataAsync(IList<MetaData> entities, CancellationToken cancellationToken)
        {
            await _metaDataRepository.UpdateMetaEntitiesAsync(entities, cancellationToken);
        }

        public async Task RemoveMetaDataAsync(ObjectId id, CancellationToken cancellationToken)
        {
            var entity = await _metaDataRepository.GetMetaEntityByIdAsync(id, cancellationToken);
            if (entity != null) 
                await _metaDataRepository.RemoveMetaEntityAsync(id, cancellationToken);
        }

        public async Task RemoveMetaDataAsync(IList<ObjectId> ids, CancellationToken cancellationToken)
        {
            await _metaDataRepository.RemoveMetaEntitiesAsync(ids, cancellationToken);
        }
    }
}