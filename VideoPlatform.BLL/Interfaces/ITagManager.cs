using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.BLL.Interfaces
{
    public interface ITagManager
    {
        Task<Tag> GetTagByIdAsync(int tagId, CancellationToken cancellationToken = default(CancellationToken));

        Task<Tag> GetTagByNameAsync(string tagName, CancellationToken cancellationToken = default(CancellationToken));

        Task<ICollection<Tag>> GetTagsAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<PagingResult<Tag>> GetTagsAsync(Paging<Tag> filter, CancellationToken cancellationToken = default(CancellationToken));

        Task<Tag> SaveTagAsync(Tag entity, CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<Tag>> AddTagsAsync(IList<Tag> tags, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateTagsAsync(IList<Tag> tags, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveTagAsync(int id, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoteTagsAsync(IList<int> tagIds, CancellationToken cancellationToken = default(CancellationToken));
    }
}