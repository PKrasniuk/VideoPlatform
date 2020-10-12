using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.AccessTokenValidation;
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

namespace VideoPlatform.Api.Controllers
{
    /// <summary>
    /// Partners Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly IPartnerManager _partnerManager;
        private readonly IIndexingPartnerManager _indexingPartnerManager;
        private readonly CacheSettings _cacheSettings;
        private readonly IMapper _mapper;

        /// <summary>
        /// Partners Controller Constructor
        /// </summary>
        /// <param name="partnerManager"></param>
        /// <param name="indexingPartnerManager"></param>
        /// <param name="cacheSettingsAccessor"></param>
        /// <param name="mapper"></param>
        public PartnersController(IPartnerManager partnerManager, IIndexingPartnerManager indexingPartnerManager,
            IOptions<CacheSettings> cacheSettingsAccessor, IMapper mapper)
        {
            _partnerManager = partnerManager ?? throw new ArgumentNullException(nameof(partnerManager));
            _indexingPartnerManager = indexingPartnerManager ?? throw new ArgumentNullException(nameof(indexingPartnerManager));
            _cacheSettings = cacheSettingsAccessor.Value ?? throw new ArgumentNullException(nameof(cacheSettingsAccessor.Value));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get Partner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PartnerModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "readAccess")]
        public async Task<ActionResult<PartnerModel>> GetPartnerAsync(int id)
        {
            var partner = await _partnerManager.GetPartnerByIdAsync(id);
            if (partner != null)
            {
                return _mapper.Map<PartnerModel>(partner);
            }

            return NotFound();
        }

        /// <summary>
        /// Get Partners By ElasticSearch Filter
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FilterResultModel<PartnerModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "readAccess")]
        public async Task<ActionResult<FilterResultModel<PartnerModel>>> GetPartnersSearchAsync([FromForm] FilterPartnerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var filter = new Filter<Partner>
            {
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                SortOrder = model.SortOrder == SortOrder.Descending
                    ? Nest.SortOrder.Descending
                    : Nest.SortOrder.Ascending,
                FilterQuery = model.FilterQuery
            };

            switch (model.SortedProperty)
            {
                case PartnerSortedProperty.Name:
                    filter.SortedProperty = x => x.Name;
                    break;
                case PartnerSortedProperty.Description:
                    filter.SortedProperty = x => x.Description;
                    break;
                case PartnerSortedProperty.Logo:
                    filter.SortedProperty = x => x.Logo;
                    break;
                case PartnerSortedProperty.ShowOnPartnerPage:
                    filter.SortedProperty = x => x.ShowOnPartnerPage;
                    break;
                case PartnerSortedProperty.IsArchived:
                    filter.SortedProperty = x => x.IsArchived;
                    break;
                default:
                    filter.SortedProperty = x => x.Id;
                    break;
            }

            var result = await _indexingPartnerManager.Find(filter);

            if (result?.Items == null || result.TotalCount == 0)
            {
                return NotFound();
            }

            return new FilterResultModel<PartnerModel>
            {
                TotalCount = result.TotalCount,
                Items = new List<PartnerModel>(result.Items.Select(x => _mapper.Map<PartnerModel>(x)))
            };
        }

        /// <summary>
        /// Get Partners By Filter
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("filter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FilterResultModel<PartnerModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "readAccess")]
        public async Task<ActionResult<FilterResultModel<PartnerModel>>> GetPartnersByFilterAsync([FromForm] FilterPartnerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var filter = new Paging<Partner>
            {
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                SortOrder = model.SortOrder == SortOrder.Descending
                    ? SortOrder.Descending
                    : SortOrder.Ascending
            };

            if (!string.IsNullOrEmpty(model.FilterQuery))
            {
                filter.FilterExpression = x =>
                    x.Name.StartsWith(model.FilterQuery) ||
                    x.Description.StartsWith(model.FilterQuery) ||
                    x.Logo.StartsWith(model.FilterQuery);
            }

            switch (model.SortedProperty)
            {
                case PartnerSortedProperty.Name:
                    filter.SortedProperty = x => x.Name;
                    break;
                case PartnerSortedProperty.Description:
                    filter.SortedProperty = x => x.Description;
                    break;
                case PartnerSortedProperty.Logo:
                    filter.SortedProperty = x => x.Logo;
                    break;
                case PartnerSortedProperty.ShowOnPartnerPage:
                    filter.SortedProperty = x => x.ShowOnPartnerPage;
                    break;
                case PartnerSortedProperty.IsArchived:
                    filter.SortedProperty = x => x.IsArchived;
                    break;
                default:
                    filter.SortedProperty = x => x.Id;
                    break;
            }

            var result = await _partnerManager.GetPartnersAsync(filter);

            if (result?.Items == null || result.TotalCount == 0)
            {
                return NotFound();
            }

            return new FilterResultModel<PartnerModel>
            {
                TotalCount = result.TotalCount,
                Items = new List<PartnerModel>(result.Items.Select(x => _mapper.Map<PartnerModel>(x)))
            };
        }

        /// <summary>
        /// Get Cached Partners
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<PartnerModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "readAccess")]
        public async Task<ActionResult<ICollection<PartnerModel>>> GetCachedPartnersAsync()
        {
            var result = await _partnerManager.GetCachedPartnersAsync(_cacheSettings.PartnersExpirationMinutes);
            if (result != null && result.Any())
            {
                return result.Select(x => _mapper.Map<PartnerModel>(x)).ToList();
            }

            return NotFound();
        }

        /// <summary>
        /// Add Partner
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PartnerModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsModel))]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "writeAccess")]
        public async Task<ActionResult<PartnerModel>> AddPartnerAsync([FromForm] AddPartnerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var partner = await _partnerManager.SavePartnerAsync(_mapper.Map<Partner>(model));
            if (partner != null)
            {
                return _mapper.Map<PartnerModel>(partner);
            }

            return UnprocessableEntity();
        }

        /// <summary>
        /// Update Partner
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PartnerModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsModel))]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "writeAccess")]
        public async Task<ActionResult<PartnerModel>> UpdatePartnerAsync([FromForm] UpdatePartnerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var partner = await _partnerManager.SavePartnerAsync(_mapper.Map<Partner>(model));
            if (partner != null)
            {
                return _mapper.Map<PartnerModel>(partner);
            }

            return UnprocessableEntity();
        }

        /// <summary>
        /// Remove Partner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "writeAccess")]
        public async Task<ActionResult> RemovePartnerAsync(int id)
        {
            var item = await _partnerManager.GetPartnerByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            await _partnerManager.RemovePartnerAsync(id);

            return Ok();
        }
    }
}