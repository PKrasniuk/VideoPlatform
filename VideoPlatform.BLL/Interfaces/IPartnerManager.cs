using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Models;

namespace VideoPlatform.BLL.Interfaces;

public interface IPartnerManager
{
    Task<Partner> GetPartnerByIdAsync(int partnerId, CancellationToken cancellationToken = default);

    Task<Partner> GetPartnerByNameAsync(string partnerName, CancellationToken cancellationToken = default);

    Task<FilterResult<Partner>> GetPartnersByElasticSearchAsync(Filter<Partner> filter,
        CancellationToken cancellationToken = default);

    Task<ICollection<Partner>> GetPartnersAsync(CancellationToken cancellationToken = default);

    Task<PagingResult<Partner>> GetPartnersAsync(Paging<Partner> filter, CancellationToken cancellationToken = default);

    Task<ICollection<Partner>> GetCachedPartnersAsync(int expirationMinutes,
        CancellationToken cancellationToken = default);

    Task<Partner> SavePartnerAsync(Partner entity, CancellationToken cancellationToken = default);

    Task RemovePartnerAsync(int id, CancellationToken cancellationToken = default);

    Task ReIndexingPartnersAsync(CancellationToken cancellationToken = default);
}