using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class InfoDataRepository(CosmosContext dbContext)
    : CosmosEntityRepository<InfoData>(dbContext, "infoData"), IInfoDataRepository;