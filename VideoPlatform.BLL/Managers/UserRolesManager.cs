using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.DataModels;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class UserRolesManager : IUserRolesManager
{
    private readonly IUserRolesRepository _userRolesRepository;

    public UserRolesManager(IUserRolesRepository userRolesRepository)
    {
        _userRolesRepository = userRolesRepository ?? throw new ArgumentNullException(nameof(userRolesRepository));
    }

    public async Task<ICollection<UserRoleDataModel>> GetUserRolesAsync(CancellationToken cancellationToken)
    {
        return await _userRolesRepository.GetUserRolesAsync(cancellationToken);
    }

    public async Task<ICollection<UserRoleDataModel>> GetUserRolesAlternativeAsync(CancellationToken cancellationToken)
    {
        return await _userRolesRepository.GetUserRolesAlternativeAsync(cancellationToken);
    }
}