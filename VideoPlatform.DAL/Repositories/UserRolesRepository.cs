using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideoPlatform.DAL.DataModels;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.DAL.Repositories;

public class UserRolesRepository : IUserRolesRepository
{
    private readonly VideoPlatformContext _dbContext;

    public UserRolesRepository(VideoPlatformContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<ICollection<UserRoleDataModel>> GetUserRolesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.UserRoleData.FromSqlRaw("EXEC GetUserRoles").AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<ICollection<UserRoleDataModel>> GetUserRolesAlternativeAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.UserRoleData.AsNoTracking().ToListAsync(cancellationToken);
    }
}