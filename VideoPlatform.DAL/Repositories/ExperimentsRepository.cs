using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class ExperimentsRepository : EntityRepository<Experiment, int>, IExperimentsRepository
{
    public ExperimentsRepository(VideoPlatformContext dbContext) : base(dbContext)
    {
    }
}