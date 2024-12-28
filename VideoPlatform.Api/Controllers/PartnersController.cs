using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VideoPlatform.Api.Models.Enums;
using VideoPlatform.Api.Models.RequestModels;
using VideoPlatform.Api.Models.ResponseModels;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.CacheService.Infrastructure.Settings;
using VideoPlatform.Common.Models.Enums;
using VideoPlatform.Common.Models.ResponseModels;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;
using VideoPlatform.ElasticSearchService.Models;

namespace VideoPlatform.Api.Controllers;

/// <summary>
///     Partners Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PartnersController : ControllerBase
{
    private readonly CacheSettings _cacheSettings;
    private readonly IIndexingPartnerManager _indexingPartnerManager;
    private readonly IMapper _mapper;
    private readonly IPartnerManager _partnerManager;

    /// <summary>
    ///     Partners Controller Constructor
    /// </summary>
    /// <param name="partnerManager"></param>
    /// <param name="indexingPartnerManager"></param>
    /// <param name="cacheSettingsAccessor"></param>
    /// <param name="mapper"></param>
    public PartnersController(IPartnerManager partnerManager, IIndexingPartnerManager indexingPartnerManager,
        IOptions<CacheSettings> cacheSettingsAccessor, IMapper mapper)
    {
        _partnerManager = partnerManager ?? throw new ArgumentNullException(nameof(partnerManager));
        _indexingPartnerManager =
            indexingPartnerManager ?? throw new ArgumentNullException(nameof(indexingPartnerManager));
        _cacheSettings = cacheSettingsAccessor.Value ?? throw new ArgumentNullException(nameof(cacheSettingsAccessor));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    ///     Get Partner
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PartnerModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = "readAccess")]
    public async Task<ActionResult<PartnerModel>> GetPartnerAsync(int id)
    {
        var partner = await _partnerManager.GetPartnerByIdAsync(id);
        return partner == null ? NotFound() : _mapper.Map<PartnerModel>(partner);
    }

    /// <summary>
    ///     Get Partners By ElasticSearch Filter
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("search")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FilterResultModel<PartnerModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = "readAccess")]
    public async Task<ActionResult<FilterResultModel<PartnerModel>>> GetPartnersSearchAsync(
        [FromForm] FilterPartnerModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var filter = new Filter<Partner>
        {
            PageNumber = model.PageNumber,
            PageSize = model.PageSize,
            SortOrder = model.SortOrder == SortOrder.Descending
                ? Nest.SortOrder.Descending
                : Nest.SortOrder.Ascending,
            FilterQuery = model.FilterQuery,
            SortedProperty = model.SortedProperty switch
            {
                PartnerSortedProperty.Name => x => x.Name,
                PartnerSortedProperty.Description => x => x.Description,
                PartnerSortedProperty.Logo => x => x.Logo,
                PartnerSortedProperty.ShowOnPartnerPage => x => x.ShowOnPartnerPage,
                PartnerSortedProperty.IsArchived => x => x.IsArchived,
                _ => x => x.Id
            }
        };

        var result = await _indexingPartnerManager.Find(filter);
        return result?.Items == null || result.TotalCount == 0
            ? NotFound()
            : new FilterResultModel<PartnerModel>
            {
                TotalCount = result.TotalCount,
                Items = new List<PartnerModel>(result.Items.Select(x => _mapper.Map<PartnerModel>(x)))
            };
    }

    /// <summary>
    ///     Get Partners By Filter
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FilterResultModel<PartnerModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = "readAccess")]
    public async Task<ActionResult<FilterResultModel<PartnerModel>>> GetPartnersByFilterAsync(
        [FromForm] FilterPartnerModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var filter = new Paging<Partner>
        {
            PageNumber = model.PageNumber,
            PageSize = model.PageSize,
            SortOrder = model.SortOrder == SortOrder.Descending
                ? SortOrder.Descending
                : SortOrder.Ascending
        };

        if (!string.IsNullOrEmpty(model.FilterQuery))
            filter.FilterExpression = x =>
                x.Name.StartsWith(model.FilterQuery) ||
                x.Description.StartsWith(model.FilterQuery) ||
                x.Logo.StartsWith(model.FilterQuery);

        filter.SortedProperty = model.SortedProperty switch
        {
            PartnerSortedProperty.Name => x => x.Name,
            PartnerSortedProperty.Description => x => x.Description,
            PartnerSortedProperty.Logo => x => x.Logo,
            PartnerSortedProperty.ShowOnPartnerPage => x => x.ShowOnPartnerPage,
            PartnerSortedProperty.IsArchived => x => x.IsArchived,
            _ => x => x.Id
        };

        var result = await _partnerManager.GetPartnersAsync(filter);
        return result?.Items == null || result.TotalCount == 0
            ? NotFound()
            : new FilterResultModel<PartnerModel>
            {
                TotalCount = result.TotalCount,
                Items = new List<PartnerModel>(result.Items.Select(x => _mapper.Map<PartnerModel>(x)))
            };
    }

    /// <summary>
    ///     Get Cached Partners
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<PartnerModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = "readAccess")]
    public async Task<ActionResult<ICollection<PartnerModel>>> GetCachedPartnersAsync()
    {
        var result = await _partnerManager.GetCachedPartnersAsync(_cacheSettings.PartnersExpirationMinutes);
        return result != null && result.Any()
            ? result.Select(x => _mapper.Map<PartnerModel>(x)).ToList()
            : NotFound();
    }

    /// <summary>
    ///     Add Partner
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PartnerModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsModel))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = "writeAccess")]
    public async Task<ActionResult<PartnerModel>> AddPartnerAsync([FromForm] AddPartnerModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var partnerModel = _mapper.Map<Partner>(model);
        partnerModel.Logo = model.Logo.FileName;
        var partner = await _partnerManager.SavePartnerAsync(partnerModel);
        return partner != null ? _mapper.Map<PartnerModel>(partner) : UnprocessableEntity();
    }

    /// <summary>
    ///     Update Partner
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PartnerModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsModel))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = "writeAccess")]
    public async Task<ActionResult<PartnerModel>> UpdatePartnerAsync([FromForm] UpdatePartnerModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var partnerModel = _mapper.Map<Partner>(model);
        partnerModel.Logo = model.Logo.FileName;
        var partner = await _partnerManager.SavePartnerAsync(partnerModel);
        return partner == null ? UnprocessableEntity() : _mapper.Map<PartnerModel>(partner);
    }

    /// <summary>
    ///     Remove Partner
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = "writeAccess")]
    public async Task<ActionResult> RemovePartnerAsync(int id)
    {
        var item = await _partnerManager.GetPartnerByIdAsync(id);
        if (item == null)
            return NotFound();

        await _partnerManager.RemovePartnerAsync(id);

        return Ok();
    }
}