using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.BLL.Interfaces
{
    public interface IMetaDataManager
    {
        Task<MetaData> GetMetaDataByIdAsync(ObjectId metaDataId, CancellationToken cancellationToken = default);

        Task<MetaData> GetMetaDataByNameAsync(string metaDataName, CancellationToken cancellationToken = default);

        Task<ICollection<MetaData>> GetMetaDataAsync(CancellationToken cancellationToken = default);

        Task<PagingResult<MetaData>> GetMetaDataAsync(MetaPaging<MetaData> filter, CancellationToken cancellationToken = default);

        Task<MetaData> SaveMetaDataAsync(MetaData entity, CancellationToken cancellationToken = default);

        Task<IList<MetaData>> AddMetaDataAsync(IList<MetaData> entities, CancellationToken cancellationToken = default);

        Task UpdateMetaDataAsync(IList<MetaData> entities, CancellationToken cancellationToken = default);

        Task RemoveMetaDataAsync(ObjectId id, CancellationToken cancellationToken = default);

        Task RemoveMetaDataAsync(IList<ObjectId> ids, CancellationToken cancellationToken = default);
    }
}