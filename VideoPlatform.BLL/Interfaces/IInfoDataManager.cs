using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.BLL.Interfaces
{
    public interface IInfoDataManager
    {
        Task<InfoData> GetInfoDataByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        Task<InfoData> GetInfoDataByNameAsync(string name, CancellationToken cancellationToken = default(CancellationToken));

        Task<ICollection<InfoData>> GetInfoDataAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<PagingResult<InfoData>> GetInfoDataAsync(Paging<InfoData> filter, CancellationToken cancellationToken = default(CancellationToken));

        Task<InfoData> SaveInfoDataAsync(InfoData entity, CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<InfoData>> AddInfoDataAsync(IList<InfoData> entities, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateInfoDataAsync(IList<InfoData> entities, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveInfoDataAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveInfoDataAsync(IList<Guid> ids, CancellationToken cancellationToken = default(CancellationToken));
    }
}