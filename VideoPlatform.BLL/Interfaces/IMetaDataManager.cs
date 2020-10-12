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
        Task<MetaData> GetMetaDataByIdAsync(ObjectId metaDataId, CancellationToken cancellationToken = default(CancellationToken));

        Task<MetaData> GetMetaDataByNameAsync(string metaDataName, CancellationToken cancellationToken = default(CancellationToken));

        Task<ICollection<MetaData>> GetMetaDataAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<PagingResult<MetaData>> GetMetaDataAsync(MetaPaging<MetaData> filter, CancellationToken cancellationToken = default(CancellationToken));

        Task<MetaData> SaveMetaDataAsync(MetaData entity, CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<MetaData>> AddMetaDataAsync(IList<MetaData> entities, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateMetaDataAsync(IList<MetaData> entities, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveMetaDataAsync(ObjectId id, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveMetaDataAsync(IList<ObjectId> ids, CancellationToken cancellationToken = default(CancellationToken));
    }
}