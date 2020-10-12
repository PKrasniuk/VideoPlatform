using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories
{
    public class InfoDataRepository : CosmosEntityRepository<InfoData>, IInfoDataRepository
    {
        public InfoDataRepository(CosmosContext dbContext) : base(dbContext, "infoData")
        {
        }
    }
}