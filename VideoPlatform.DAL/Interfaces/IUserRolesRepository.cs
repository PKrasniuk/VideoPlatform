using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.DAL.DataModels;

namespace VideoPlatform.DAL.Interfaces
{
    public interface IUserRolesRepository
    {
        Task<ICollection<UserRoleDataModel>> GetUserRolesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<ICollection<UserRoleDataModel>> GetUserRolesAlternativeAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}