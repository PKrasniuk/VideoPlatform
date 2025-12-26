using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class ExperimentsRepository(VideoPlatformContext dbContext)
    : EntityRepository<Experiment, int>(dbContext), IExperimentsRepository;