using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.BLL.Interfaces;

public interface IInfoDataManager
{
    Task<InfoData> GetInfoDataByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<InfoData> GetInfoDataByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<ICollection<InfoData>> GetInfoDataAsync(CancellationToken cancellationToken = default);

    Task<PagingResult<InfoData>> GetInfoDataAsync(Paging<InfoData> filter,
        CancellationToken cancellationToken = default);

    Task<InfoData> SaveInfoDataAsync(InfoData entity, CancellationToken cancellationToken = default);

    Task<IList<InfoData>> AddInfoDataAsync(IList<InfoData> entities, CancellationToken cancellationToken = default);

    Task UpdateInfoDataAsync(IList<InfoData> entities, CancellationToken cancellationToken = default);

    Task RemoveInfoDataAsync(Guid id, CancellationToken cancellationToken = default);

    Task RemoveInfoDataAsync(IList<Guid> ids, CancellationToken cancellationToken = default);
}