using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VideoPlatform.Api.Models.RequestModels;
using VideoPlatform.Api.Models.ResponseModels;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Common.Models.ResponseModels;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.Api.Controllers
{
    /// <summary>
    /// InfoDataController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InfoDataController : ControllerBase
    {
        private readonly IInfoDataManager _infoDataManager;
        private readonly IMapper _mapper;
        private readonly ILogger<InfoDataController> _logger;

        /// <summary>
        /// InfoDataController constructor
        /// </summary>
        /// <param name="infoDataManager"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public InfoDataController(IInfoDataManager infoDataManager, IMapper mapper, ILogger<InfoDataController> logger)
        {
            _infoDataManager = infoDataManager ?? throw new ArgumentNullException(nameof(infoDataManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get InfoData
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<InfoDataModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
        [ResponseCache(Duration = ConfigurationConstants.StaticCacheSeconds, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<ICollection<InfoDataModel>>> GetInfoDataAsync()
        {
            _logger.LogInformation("The method GetInfoDataAsync");

            var entities = await _infoDataManager.GetInfoDataAsync();
            if (entities != null && entities.Any())
            {
                return _mapper.Map<Collection<InfoDataModel>>(entities);
            }

            return NotFound();
        }

        /// <summary>
        /// Get InfoData by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoDataModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
        public async Task<ActionResult<InfoDataModel>> GetInfoDataByIdAsync(string id)
        {
            _logger.LogInformation("The method GetInfoDataByIdAsync");

            var item = await _infoDataManager.GetInfoDataByIdAsync(new Guid(id));
            if (item != null)
            {
                return _mapper.Map<InfoDataModel>(item);
            }

            return NotFound();
        }

        /// <summary>
        /// Add InfoData
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoDataModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsModel))]
        public async Task<ActionResult<InfoDataModel>> AddInfoDataAsync([FromForm] AddInfoDataModel model)
        {
            _logger.LogInformation("The method AddInfoDataAsync");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _infoDataManager.SaveInfoDataAsync(_mapper.Map<InfoData>(model));
            if (item != null)
            {
                return _mapper.Map<InfoDataModel>(item);
            }

            return UnprocessableEntity();
        }

        /// <summary>
        /// Update InfoData
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
        public async Task<ActionResult> UpdateInfoDataAsync([FromForm] UpdateInfoDataModel model)
        {
            _logger.LogInformation("The method UpdateInfoDataAsync");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _infoDataManager.SaveInfoDataAsync(_mapper.Map<InfoData>(model));

            return Ok();
        }

        /// <summary>
        /// Remove InfoData
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
        public async Task<ActionResult> RemoveInfoDataAsync(string id)
        {
            _logger.LogInformation("The method RemoveInfoDataAsync");

            var entityId = new Guid(id);

            var item = await _infoDataManager.GetInfoDataByIdAsync(entityId);
            if (item == null)
            {
                return NotFound();
            }

            await _infoDataManager.RemoveInfoDataAsync(entityId);

            return Ok();
        }
    }
}