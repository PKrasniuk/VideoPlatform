using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.CacheService.Interfaces;
using VideoPlatform.DAL.Interfaces;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;
using VideoPlatform.ElasticSearchService.Models;

namespace VideoPlatform.BLL.Managers
{
    public class PartnerManager : IPartnerManager
    {
        private readonly IPartnersRepository _partnersRepository;
        private readonly IIndexingPartnerManager _indexingPartnerManager;
        private readonly ICacheRepository _cacheRepository;

        private const string PartnersCacheKey = "AllPartners";

        public PartnerManager(IPartnersRepository partnersRepository, IIndexingPartnerManager indexingPartnerManager, ICacheRepository cacheRepository)
        {
            _partnersRepository = partnersRepository ?? throw new ArgumentNullException(nameof(partnersRepository));
            _indexingPartnerManager = indexingPartnerManager ?? throw new ArgumentNullException(nameof(indexingPartnerManager));
            _cacheRepository = cacheRepository ?? throw new ArgumentNullException(nameof(cacheRepository));
        }

        public async Task<Partner> GetPartnerByIdAsync(int partnerId, CancellationToken cancellationToken)
        {
            return await _partnersRepository.GetEntityByIdAsync(partnerId, cancellationToken);
        }

        public async Task<Partner> GetPartnerByNameAsync(string partnerName, CancellationToken cancellationToken)
        {
            return await _partnersRepository.GetEntityAsync(x => x.Name.Equals(partnerName), cancellationToken);
        }

        public async Task<FilterResult<Partner>> GetPartnersByElasticSearchAsync(Filter<Partner> filter, CancellationToken cancellationToken)
        {
            return await _indexingPartnerManager.Find(filter, cancellationToken);
        }

        public async Task<ICollection<Partner>> GetPartnersAsync(CancellationToken cancellationToken)
        {
            return await _partnersRepository.GetEntitiesAsync(null, cancellationToken);
        }

        public async Task<PagingResult<Partner>> GetPartnersAsync(Paging<Partner> filter, CancellationToken cancellationToken)
        {
            return await _partnersRepository.GetPagingEntitiesAsync(filter, cancellationToken);
        }

        public async Task<ICollection<Partner>> GetCachedPartnersAsync(int expirationMinutes, CancellationToken cancellationToken)
        {
            var isCacheExist = await _cacheRepository.ExistObjectAsync(PartnersCacheKey, cancellationToken);
            if (isCacheExist)
                return await _cacheRepository.GetObjectAsync<Collection<Partner>>(PartnersCacheKey, cancellationToken);

            var items = await _partnersRepository.GetEntitiesAsync(null, cancellationToken);
            if (items != null && items.Any())
                await _cacheRepository.SetObjectAsync(PartnersCacheKey, items, expirationMinutes, cancellationToken);

            return items;
        }

        public async Task<Partner> SavePartnerAsync(Partner entity, CancellationToken cancellationToken)
        {
            if (entity.Id > 0)
            {
                await _partnersRepository.UpdateEntityAsync(entity, cancellationToken);

                await _indexingPartnerManager.UpdateEntityAsync(entity, cancellationToken);

                return entity;
            }

            var result = await _partnersRepository.CreateEntityAsync(entity, cancellationToken);

            await _indexingPartnerManager.IndexEntityAsync(result, cancellationToken);

            return result;
        }

        public async Task RemovePartnerAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _partnersRepository.GetEntityByIdAsync(id, cancellationToken);
            if (entity != null)
            {
                await _partnersRepository.RemoveEntityAsync(id, cancellationToken);

                await _indexingPartnerManager.RemoveEntityAsync(entity, cancellationToken);
            }
        }

        public async Task ReIndexingPartnersAsync(CancellationToken cancellationToken)
        {
            var partners = await _partnersRepository.GetEntitiesAsync(null, cancellationToken);
            if (partners != null && partners.Any()) 
                await _indexingPartnerManager.ReIndex(partners, cancellationToken);
        }
    }
}