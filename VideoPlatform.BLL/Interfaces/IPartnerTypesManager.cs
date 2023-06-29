using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.BLL.Models;
using VideoPlatform.Domain.Entities;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.BLL.Interfaces;

public interface IPartnerTypesManager
{
    ICollection<ListItem<byte>> GetPartnerTypes(CancellationToken cancellationToken = default);

    Task<ICollection<ListItem<byte>>> GetPartnerTypesByPartnerIdAsync(int partnerId,
        CancellationToken cancellationToken = default);

    Task<ICollection<PartnerTypes>> GetPartnerTypesCollectionAsync(CancellationToken cancellationToken = default);

    Task<ICollection<PartnerTypes>> GetPartnerTypesCollectionAsync(int partnerId,
        CancellationToken cancellationToken = default);

    Task<PartnerTypes> AddPartnerTypeAsync(int partnerId, PartnerType type,
        CancellationToken cancellationToken = default);

    Task RemovePartnerTypeAsync(int partnerId, PartnerType type, CancellationToken cancellationToken = default);

    Task RemovePartnerTypeByEventBusAsync(int partnerId, PartnerType type,
        CancellationToken cancellationToken = default);

    Task RemovePartnerTypeByRabbitMQAsync(int partnerId, PartnerType type,
        CancellationToken cancellationToken = default);

    Task RemovePartnerTypeByKafkaAsync(int partnerId, PartnerType type, CancellationToken cancellationToken = default);

    Task ReIndexingPartnerTypesAsync(CancellationToken cancellationToken = default);
}