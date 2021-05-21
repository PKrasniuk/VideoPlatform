using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using VideoPlatform.Common.Models.Enums;
using VideoPlatform.DAL.Infrastructure.Helpers;
using VideoPlatform.DAL.Interfaces;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories
{
    public abstract class MetaEntityRepository<TEntity> : IMetaEntityRepository<TEntity> where TEntity : MetaEntity
    {
        public MongoClient Client { get; }

        public IMongoDatabase MetaDatabase { get; }

        private readonly IMongoCollection<TEntity> _collection;

        protected MetaEntityRepository(MetaContext metaContext)
        {
            if (metaContext?.Client != null && metaContext.MetaDatabase != null)
            {
                Client = metaContext.Client;
                MetaDatabase = metaContext.MetaDatabase;
            }
            else
            {
                throw new ArgumentNullException(nameof(metaContext));
            }
        }

        protected MetaEntityRepository(MetaContext metaContext, string collectionName)
        {
            if (metaContext?.Client != null && metaContext.MetaDatabase != null && !string.IsNullOrEmpty(collectionName))
            {
                Client = metaContext.Client;
                MetaDatabase = metaContext.MetaDatabase;
                _collection = MetaDatabase.GetCollection<TEntity>(collectionName);
            }
            else
            {
                throw new ArgumentNullException(nameof(metaContext));
            }
        }

        public async Task<TEntity> GetMetaEntityByIdAsync(ObjectId id, CancellationToken cancellationToken)
        {
            return await _collection.Find(x => x.Id.Equals(id)).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<TEntity> GetMetaEntityAsync(Expression<Func<TEntity, bool>> filterExpression = null, CancellationToken cancellationToken = default)
        {
            return filterExpression != null
                ? await _collection.Find(filterExpression).SingleOrDefaultAsync(cancellationToken)
                : await _collection.Find(_ => true).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<ICollection<TEntity>> GetMetaEntitiesAsync(Expression<Func<TEntity, bool>> filterExpression = null, CancellationToken cancellationToken = default)
        {
            return filterExpression != null
                ? await _collection.Find(filterExpression).ToListAsync(cancellationToken)
                : await _collection.Find(_ => true).ToListAsync(cancellationToken);
        }

        public async Task<PagingResult<TEntity>> GetPagingMetaEntitiesAsync(MetaPaging<TEntity> pagingModel, CancellationToken cancellationToken)
        {
            var query = pagingModel.FilterExpression != null
                ? _collection.Find(pagingModel.FilterExpression)
                : _collection.Find(_ => true);

            var sortOrder = !string.IsNullOrWhiteSpace(pagingModel.SortedProperty)
                ? pagingModel.SortedProperty.FirstCharToUpper()
                : "UpdatedAt";

            query = pagingModel.SortOrder switch
            {
                SortOrder.None => query.Sort(Builders<TEntity>.Sort.Ascending(sortOrder)),
                SortOrder.Ascending => query.Sort(Builders<TEntity>.Sort.Ascending(sortOrder)),
                SortOrder.Descending => query.Sort(Builders<TEntity>.Sort.Descending(sortOrder)),
                _ => query
            };

            return new PagingResult<TEntity>
            {
                Items = await query.Skip((pagingModel.PageNumber - 1) * pagingModel.PageSize)
                    .Limit(pagingModel.PageSize).ToListAsync(cancellationToken),
                TotalCount = await query.CountDocumentsAsync(cancellationToken)
            };
        }

        public async Task<bool> IsMetaEntityExistAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return await _collection.Find(x => x.Id.Equals(entity.Id)).SingleOrDefaultAsync(cancellationToken) != null;
        }

        public async Task<TEntity> CreateMetaEntityAsync(TEntity entity, CancellationToken cancellationToken)
        {
            using var session = await Client.StartSessionAsync(cancellationToken: cancellationToken);
            try
            {
                session.StartTransaction(
                    new TransactionOptions(writeConcern: new Optional<WriteConcern>(WriteConcern.WMajority)));

                entity.CreatedAt = new BsonDateTime(DateTime.Now);
                entity.UpdatedAt = entity.CreatedAt;
                await _collection.InsertOneAsync(entity, new InsertOneOptions
                {
                    BypassDocumentValidation = true
                }, cancellationToken);

                await session.CommitTransactionAsync(cancellationToken);

                return entity;
            }
            catch (Exception)
            {
                await session.AbortTransactionAsync(cancellationToken);

                return default;
            }
        }

        public async Task<IList<TEntity>> CreateMetaEntitiesAsync(IList<TEntity> entities, CancellationToken cancellationToken)
        {
            using var session = await Client.StartSessionAsync(cancellationToken: cancellationToken);
            session.StartTransaction(
                new TransactionOptions(writeConcern: new Optional<WriteConcern>(WriteConcern.WMajority)));

            try
            {
                var dataTime = new BsonDateTime(DateTime.Now);
                foreach (var entity in entities)
                {
                    entity.CreatedAt = dataTime;
                    entity.UpdatedAt = dataTime;
                }

                await _collection.InsertManyAsync(entities, new InsertManyOptions
                {
                    IsOrdered = false,
                    BypassDocumentValidation = true
                }, cancellationToken);

                await session.CommitTransactionAsync(cancellationToken);

                return entities;
            }
            catch (Exception)
            {
                await session.AbortTransactionAsync(cancellationToken);

                return default;
            }
        }

        public async Task<bool> UpdateMetaEntityAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var operationStatus = false;

            using var session = await Client.StartSessionAsync(cancellationToken: cancellationToken);
            session.StartTransaction(
                new TransactionOptions(writeConcern: new Optional<WriteConcern>(WriteConcern.Acknowledged)));

            try
            {
                var dbEntity = await _collection.Find(x => x.Id.Equals(entity.Id)).SingleOrDefaultAsync(cancellationToken);
                if (dbEntity != null)
                {
                    entity.UpdatedAt = new BsonDateTime(DateTime.Now);
                    entity.CreatedAt = dbEntity.CreatedAt;

                    var result = await _collection.ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity,
                        new ReplaceOptions
                        {
                            IsUpsert = true,
                            BypassDocumentValidation = true
                        }, cancellationToken);

                    operationStatus = result.IsAcknowledged;
                }

                await session.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception)
            {
                await session.AbortTransactionAsync(cancellationToken);
            }

            return operationStatus;
        }

        public async Task<bool> UpdateMetaEntitiesAsync(IList<TEntity> entities, CancellationToken cancellationToken)
        {
            var operationStatus = true;

            var dataTime = new BsonDateTime(DateTime.Now);

            using var session = await Client.StartSessionAsync(cancellationToken: cancellationToken);
            session.StartTransaction(
                new TransactionOptions(writeConcern: new Optional<WriteConcern>(WriteConcern.Acknowledged)));

            try
            {
                foreach (var entity in entities)
                {
                    var dbEntity = await _collection.Find(x => x.Id.Equals(entity.Id)).SingleOrDefaultAsync(cancellationToken);
                    if (dbEntity != null)
                    {
                        entity.UpdatedAt = dataTime;
                        entity.CreatedAt = dbEntity.CreatedAt;

                        var result = await _collection.ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity,
                            new ReplaceOptions
                            {
                                IsUpsert = true,
                                BypassDocumentValidation = true
                            }, cancellationToken);

                        operationStatus = operationStatus && result.IsAcknowledged;
                    }
                    else
                    {
                        operationStatus = false;
                    }
                }

                await session.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception)
            {
                await session.AbortTransactionAsync(cancellationToken);

                operationStatus = false;
            }

            return operationStatus;
        }

        public async Task<bool> RemoveMetaEntityAsync(ObjectId id, CancellationToken cancellationToken)
        {
            var operationStatus = false;

            using var session = await Client.StartSessionAsync(cancellationToken: cancellationToken);
            session.StartTransaction(
                new TransactionOptions(writeConcern: new Optional<WriteConcern>(WriteConcern.Acknowledged)));

            try
            {
                var result = await _collection.DeleteOneAsync(x => x.Id.Equals(id), cancellationToken);

                operationStatus = result.IsAcknowledged;

                await session.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception)
            {
                await session.AbortTransactionAsync(cancellationToken);
            }

            return operationStatus;
        }

        public async Task<bool> RemoveMetaEntitiesAsync(IList<ObjectId> ids, CancellationToken cancellationToken)
        {
            var operationStatus = false;

            using var session = await Client.StartSessionAsync(cancellationToken: cancellationToken);
            session.StartTransaction(
                new TransactionOptions(writeConcern: new Optional<WriteConcern>(WriteConcern.Acknowledged)));

            try
            {
                var result = await _collection.DeleteManyAsync(x => ids.Contains(x.Id), cancellationToken);

                operationStatus = result.IsAcknowledged;

                await session.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception)
            {
                await session.AbortTransactionAsync(cancellationToken);
            }

            return operationStatus;
        }
    }
}