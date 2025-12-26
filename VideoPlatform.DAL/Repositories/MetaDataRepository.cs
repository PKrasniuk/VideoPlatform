using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class MetaDataRepository(MetaContext metaContext)
    : MetaEntityRepository<MetaData>(metaContext, "metaData"), IMetaDataRepository;