using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.BLL.Models;
using VideoPlatform.Domain.Entities;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.BLL.Interfaces
{
    public interface IPartnerTypesManager
    {
        ICollection<ListItem<byte>> GetPartnerTypes(CancellationToken cancellationToken = default(CancellationToken));

        Task<ICollection<ListItem<byte>>> GetPartnerTypesByPartnerIdAsync(int partnerId, CancellationToken cancellationToken = default(CancellationToken));

        Task<ICollection<PartnerTypes>> GetPartnerTypesCollectionAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<ICollection<PartnerTypes>> GetPartnerTypesCollectionAsync(int partnerId, CancellationToken cancellationToken = default(CancellationToken));

        Task<PartnerTypes> AddPartnerTypeAsync(int partnerId, PartnerType type, CancellationToken cancellationToken = default(CancellationToken));

        Task RemovePartnerTypeAsync(int partnerId, PartnerType type, CancellationToken cancellationToken = default(CancellationToken));

        Task RemovePartnerTypeByEventBusAsync(int partnerId, PartnerType type, CancellationToken cancellationToken = default(CancellationToken));

        Task RemovePartnerTypeByRabbitMQAsync(int partnerId, PartnerType type, CancellationToken cancellationToken = default(CancellationToken));

        Task RemovePartnerTypeByKafkaAsync(int partnerId, PartnerType type, CancellationToken cancellationToken = default(CancellationToken));

        Task ReIndexingPartnerTypesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}