﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Interfaces;

public interface IMetaEntityRepository<TEntity> where TEntity : MetaEntity
{
    Task<TEntity> GetMetaEntityByIdAsync(ObjectId id, CancellationToken cancellationToken = default);

    Task<TEntity> GetMetaEntityAsync(Expression<Func<TEntity, bool>> filterExpression = null,
        CancellationToken cancellationToken = default);

    Task<ICollection<TEntity>> GetMetaEntitiesAsync(Expression<Func<TEntity, bool>> filterExpression = null,
        CancellationToken cancellationToken = default);

    Task<PagingResult<TEntity>> GetPagingMetaEntitiesAsync(MetaPaging<TEntity> pagingModel,
        CancellationToken cancellationToken = default);

    Task<bool> IsMetaEntityExistAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> CreateMetaEntityAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<IList<TEntity>>
        CreateMetaEntitiesAsync(IList<TEntity> entities, CancellationToken cancellationToken = default);

    Task<bool> UpdateMetaEntityAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<bool> UpdateMetaEntitiesAsync(IList<TEntity> entities, CancellationToken cancellationToken = default);

    Task<bool> RemoveMetaEntityAsync(ObjectId id, CancellationToken cancellationToken = default);

    Task<bool> RemoveMetaEntitiesAsync(IList<ObjectId> ids, CancellationToken cancellationToken = default);
}