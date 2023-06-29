using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class MetaDataRepository : MetaEntityRepository<MetaData>, IMetaDataRepository
{
    public MetaDataRepository(MetaContext metaContext) : base(metaContext, "metaData")
    {
    }
}