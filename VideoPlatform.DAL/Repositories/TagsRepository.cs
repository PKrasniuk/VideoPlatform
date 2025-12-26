using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class TagsRepository(VideoPlatformContext dbContext) : EntityRepository<Tag, int>(dbContext), ITagsRepository;