using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class ToolsRepository(VideoPlatformContext dbContext) : EntityRepository<Tool, int>(dbContext), IToolsRepository;