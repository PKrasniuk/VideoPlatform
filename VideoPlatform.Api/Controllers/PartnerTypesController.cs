using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using VideoPlatform.Api.Models.RequestModels;
using VideoPlatform.Api.Models.ResponseModels;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Common.Models.ResponseModels;

namespace VideoPlatform.Api.Controllers
{
    /// <summary>
    /// PartnerTypes Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerTypesController : ControllerBase
    {
        private readonly IPartnerTypesManager _partnerTypesManager;
        private readonly IMapper _mapper;

        /// <summary>
        /// PartnerTypes Controller Constructor
        /// </summary>
        /// <param name="partnerTypesManager"></param>
        /// <param name="mapper"></param>
        public PartnerTypesController(IPartnerTypesManager partnerTypesManager, IMapper mapper)
        {
            _partnerTypesManager = partnerTypesManager ?? throw new ArgumentNullException(nameof(partnerTypesManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get PartnerTypes
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ListItemModel<byte>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
        [ResponseCache(Duration = ConfigurationConstants.StaticCacheSeconds, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<ICollection<ListItemModel<byte>>>> GetPartnerTypesAsync()
        {
            await Task.Delay(1);

            var partnerTypes = _partnerTypesManager.GetPartnerTypes();
            if (partnerTypes != null && partnerTypes.Any())
            {
                var result = new Collection<ListItemModel<byte>>();
                foreach (var partnerType in partnerTypes)
                {
                    result.Add(new ListItemModel<byte>
                    {
                        Id = partnerType.Id,
                        Name = partnerType.Name
                    });
                }

                return result;
            }

            return NotFound();
        }

        /// <summary>
        /// Get PartnerTypes For Partner
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        [HttpGet("partner/{partnerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ListItemModel<byte>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
        public async Task<ActionResult<ICollection<ListItemModel<byte>>>> GetPartnerTypesForPartnerAsync(int partnerId)
        {
            var partnerTypes = await _partnerTypesManager.GetPartnerTypesByPartnerIdAsync(partnerId);
            if (partnerTypes != null && EnumerableExtensions.Any(partnerTypes))
            {
                var result = new Collection<ListItemModel<byte>>();
                foreach (var partnerType in partnerTypes)
                {
                    result.Add(new ListItemModel<byte>
                    {
                        Id = partnerType.Id,
                        Name = partnerType.Name
                    });
                }

                return result;
            }

            return NotFound();
        }

        /// <summary>
        /// Add PartnerType
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PartnerTypesModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsModel))]
        public async Task<ActionResult<PartnerTypesModel>> AddPartnerTypeAsync([FromForm] AddPartnerTypesModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var partnerTypes = await _partnerTypesManager.AddPartnerTypeAsync(model.PartnerId, model.Type);
            if (partnerTypes != null)
            {
                return _mapper.Map<PartnerTypesModel>(partnerTypes);
            }

            return UnprocessableEntity();
        }

        /// <summary>
        /// Remove PartnerType
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
        public async Task<ActionResult> RemovePartnerTypeAsync([FromForm] RemovePartnerTypesModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _partnerTypesManager.RemovePartnerTypeAsync(model.PartnerId, model.Type);

            return Ok();
        }

        /// <summary>
        /// ReIndexing PartnerTypes
        /// </summary>
        /// <returns></returns>
        [HttpPost("reindex")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ReIndexingPartnerTypesAsync()
        {
            await _partnerTypesManager.ReIndexingPartnerTypesAsync();

            return Ok();
        }
    }
}