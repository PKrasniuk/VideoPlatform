using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class ToolsRepository : EntityRepository<Tool, int>, IToolsRepository
{
    public ToolsRepository(VideoPlatformContext dbContext) : base(dbContext)
    {
    }
}