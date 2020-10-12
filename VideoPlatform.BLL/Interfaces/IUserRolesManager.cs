using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.DAL.DataModels;

namespace VideoPlatform.BLL.Interfaces
{
    public interface IUserRolesManager
    {
        Task<ICollection<UserRoleDataModel>> GetUserRolesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<ICollection<UserRoleDataModel>> GetUserRolesAlternativeAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}