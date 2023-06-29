using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.BLL.Managers;

public class TagManager : ITagManager
{
    private readonly ITagsRepository _tagsRepository;

    public TagManager(ITagsRepository tagsRepository)
    {
        _tagsRepository = tagsRepository ?? throw new ArgumentNullException(nameof(tagsRepository));
    }

    public async Task<Tag> GetTagByIdAsync(int tagId, CancellationToken cancellationToken)
    {
        return await _tagsRepository.GetEntityByIdAsync(tagId, cancellationToken);
    }

    public async Task<Tag> GetTagByNameAsync(string tagName, CancellationToken cancellationToken)
    {
        return await _tagsRepository.GetEntityAsync(x => x.Name.Equals(tagName), cancellationToken);
    }

    public async Task<ICollection<Tag>> GetTagsAsync(CancellationToken cancellationToken)
    {
        return await _tagsRepository.GetEntitiesAsync(null, cancellationToken);
    }

    public async Task<PagingResult<Tag>> GetTagsAsync(Paging<Tag> filter, CancellationToken cancellationToken)
    {
        return await _tagsRepository.GetPagingEntitiesAsync(filter, cancellationToken);
    }

    public async Task<Tag> SaveTagAsync(Tag entity, CancellationToken cancellationToken)
    {
        if (entity.Id > 0)
        {
            await _tagsRepository.UpdateEntityAsync(entity, cancellationToken);

            return entity;
        }

        var result = await _tagsRepository.CreateEntityAsync(entity, cancellationToken);

        return result;
    }

    public async Task<IList<Tag>> AddTagsAsync(IList<Tag> tags, CancellationToken cancellationToken)
    {
        return await _tagsRepository.CreateEntitiesAsync(tags, cancellationToken);
    }

    public async Task UpdateTagsAsync(IList<Tag> tags, CancellationToken cancellationToken)
    {
        await _tagsRepository.UpdateEntitiesAsync(tags, cancellationToken);
    }

    public async Task RemoveTagAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _tagsRepository.GetEntityByIdAsync(id, cancellationToken);
        if (entity != null)
            await _tagsRepository.RemoveEntityAsync(id, cancellationToken);
    }

    public async Task RemoteTagsAsync(IList<int> tagIds, CancellationToken cancellationToken)
    {
        await _tagsRepository.RemoveEntitiesAsync(tagIds, cancellationToken);
    }
}