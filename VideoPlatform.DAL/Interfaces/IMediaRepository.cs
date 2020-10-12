using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Interfaces
{
    public interface IMediaRepository : IEntityRepository<Media, long>
    {
    }
}