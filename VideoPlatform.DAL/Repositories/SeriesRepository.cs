using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class SeriesRepository(VideoPlatformContext dbContext)
    : EntityRepository<Series, int>(dbContext), ISeriesRepository;