using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoPlatform.Api.Models.Enums;
using VideoPlatform.Api.Models.RequestModels;
using VideoPlatform.Api.Models.ResponseModels;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.Common.Models.Enums;
using VideoPlatform.Common.Models.ResponseModels;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.Api.Controllers;

/// <summary>
///     Tags Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITagManager _tagManager;

    /// <summary>
    ///     Tags Controller Constructor
    /// </summary>
    /// <param name="tagManager"></param>
    /// <param name="mapper"></param>
    public TagsController(ITagManager tagManager, IMapper mapper)
    {
        _tagManager = tagManager ?? throw new ArgumentNullException(nameof(tagManager));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    ///     Get Tag
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TagModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
    public async Task<ActionResult<TagModel>> GetTagAsync(int id)
    {
        var item = await _tagManager.GetTagByIdAsync(id);
        return item != null ? _mapper.Map<TagModel>(item) : NotFound();
    }

    /// <summary>
    ///     Get Tag By Name
    /// </summary>
    /// <param name="tagName"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TagModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
    public async Task<ActionResult<TagModel>> GetTagByNameAsync([FromQuery(Name = "tagName")] string tagName)
    {
        var item = await _tagManager.GetTagByNameAsync(tagName);
        return item != null ? _mapper.Map<TagModel>(item) : NotFound();
    }

    /// <summary>
    ///     Get Tags
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<TagModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
    public async Task<ActionResult<ICollection<TagModel>>> GetTagsAsync()
    {
        var result = await _tagManager.GetTagsAsync();
        return result != null && result.Any()
            ? result.Select(x => _mapper.Map<TagModel>(x)).ToList()
            : NotFound();
    }

    /// <summary>
    ///     Get Partners By Filter
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FilterResultModel<TagModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
    public async Task<ActionResult<FilterResultModel<TagModel>>> GetPartnersByFilterAsync(
        [FromForm] FilterTagModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var filter = new Paging<Tag>
        {
            PageNumber = model.PageNumber,
            PageSize = model.PageSize,
            SortOrder = model.SortOrder == SortOrder.Descending
                ? SortOrder.Descending
                : SortOrder.Ascending
        };

        if (!string.IsNullOrEmpty(model.FilterQuery))
            filter.FilterExpression = x => x.Name.StartsWith(model.FilterQuery);

        filter.SortedProperty = model.SortedProperty switch
        {
            TagSortedProperty.Name => x => x.Name,
            _ => x => x.Id
        };

        var result = await _tagManager.GetTagsAsync(filter);

        return result?.Items == null || result.TotalCount == 0
            ? NotFound()
            : new FilterResultModel<TagModel>
            {
                TotalCount = result.TotalCount,
                Items = new List<TagModel>(result.Items.Select(x => _mapper.Map<TagModel>(x)))
            };
    }

    /// <summary>
    ///     Add Tag
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("addTag")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TagModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsModel))]
    public async Task<ActionResult<TagModel>> AddTagAsync([FromForm] AddTagModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var item = await _tagManager.SaveTagAsync(_mapper.Map<Tag>(model));
        return item != null ? _mapper.Map<TagModel>(item) : UnprocessableEntity();
    }

    /// <summary>
    ///     Add Tags
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("addTags")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<TagModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsModel))]
    public async Task<ActionResult<ICollection<TagModel>>> AddTagsAsync([FromBody] AddTagsModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var items = await _tagManager.AddTagsAsync(
            model.Tags.Select(modelTag => _mapper.Map<Tag>(modelTag)).ToList());
        return items != null && items.Any()
            ? items.Select(item => _mapper.Map<TagModel>(item)).ToList()
            : UnprocessableEntity();
    }

    /// <summary>
    ///     Update Tag
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TagModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsModel))]
    public async Task<ActionResult<TagModel>> UpdateTagAsync([FromForm] UpdateTagModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var item = await _tagManager.SaveTagAsync(_mapper.Map<Tag>(model));
        return item != null ? _mapper.Map<TagModel>(item) : UnprocessableEntity();
    }

    /// <summary>
    ///     Update Tags
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("updateTags")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
    public async Task<ActionResult> UpdateTagsAsync([FromBody] UpdateTagsModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _tagManager.UpdateTagsAsync(model.Tags.Select(modelTag => _mapper.Map<Tag>(modelTag)).ToList());

        return Ok();
    }

    /// <summary>
    ///     Remove Tag
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
    public async Task<ActionResult> RemoveTagAsync(int id)
    {
        var item = await _tagManager.GetTagByIdAsync(id);
        if (item == null)
            return NotFound();

        await _tagManager.RemoveTagAsync(id);

        return Ok();
    }

    /// <summary>
    ///     Remove Tags
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
    public async Task<ActionResult> RemoveTagsAsync([FromBody] RemoveTagsModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _tagManager.RemoteTagsAsync(model.Ids);

        return Ok();
    }
}