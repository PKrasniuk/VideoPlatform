using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class TagsRepository : EntityRepository<Tag, int>, ITagsRepository
{
    public TagsRepository(VideoPlatformContext dbContext) : base(dbContext)
    {
    }
}