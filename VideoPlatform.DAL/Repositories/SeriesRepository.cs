using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories
{
    public class SeriesRepository : EntityRepository<Series, int>, ISeriesRepository
    {
        public SeriesRepository(VideoPlatformContext dbContext) : base(dbContext)
        {
        }
    }
}