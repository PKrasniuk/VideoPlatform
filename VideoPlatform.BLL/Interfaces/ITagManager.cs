using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.BLL.Interfaces
{
    public interface ITagManager
    {
        Task<Tag> GetTagByIdAsync(int tagId, CancellationToken cancellationToken = default);

        Task<Tag> GetTagByNameAsync(string tagName, CancellationToken cancellationToken = default);

        Task<ICollection<Tag>> GetTagsAsync(CancellationToken cancellationToken = default);

        Task<PagingResult<Tag>> GetTagsAsync(Paging<Tag> filter, CancellationToken cancellationToken = default);

        Task<Tag> SaveTagAsync(Tag entity, CancellationToken cancellationToken = default);

        Task<IList<Tag>> AddTagsAsync(IList<Tag> tags, CancellationToken cancellationToken = default);

        Task UpdateTagsAsync(IList<Tag> tags, CancellationToken cancellationToken = default);

        Task RemoveTagAsync(int id, CancellationToken cancellationToken = default);

        Task RemoteTagsAsync(IList<int> tagIds, CancellationToken cancellationToken = default);
    }
}