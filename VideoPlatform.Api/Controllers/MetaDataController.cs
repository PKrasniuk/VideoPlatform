using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using VideoPlatform.Api.Models.RequestModels;
using VideoPlatform.Api.Models.ResponseModels;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Common.Models.ResponseModels;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.Api.Controllers
{
    /// <summary>
    /// MetaDataController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MetaDataController : ControllerBase
    {
        private readonly IMetaDataManager _metaDataManager;
        private readonly IMapper _mapper;
        private readonly ILogger<MetaDataController> _logger;

        /// <summary>
        /// MetaDataController constructor
        /// </summary>
        /// <param name="metaDataManager"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public MetaDataController(IMetaDataManager metaDataManager, IMapper mapper, ILogger<MetaDataController> logger)
        {
            _metaDataManager = metaDataManager ?? throw new ArgumentNullException(nameof(metaDataManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get MetaData
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<MetaDataModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
        [ResponseCache(Duration = ConfigurationConstants.StaticCacheSeconds, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<ICollection<MetaDataModel>>> GetMetaDataAsync()
        {
            _logger.LogInformation("The method GetMetaDataAsync");

            var entities = await _metaDataManager.GetMetaDataAsync();
            if (entities != null && entities.Any())
            {
                return _mapper.Map<Collection<MetaDataModel>>(entities);
            }

            return NotFound();
        }

        /// <summary>
        /// Get MetaData by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MetaDataModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
        public async Task<ActionResult<MetaDataModel>> GetMetaDataByIdAsync(string id)
        {
            _logger.LogInformation("The method GetMetaDataByIdAsync");

            var item = await _metaDataManager.GetMetaDataByIdAsync(new ObjectId(id));
            if (item != null)
            {
                return _mapper.Map<MetaDataModel>(item);
            }

            return NotFound();
        }

        /// <summary>
        /// Add MetaData
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MetaDataModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsModel))]
        public async Task<ActionResult<MetaDataModel>> AddMetaDataAsync([FromForm] AddMetaDataModel model)
        {
            _logger.LogInformation("The method AddMetaDataAsync");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _metaDataManager.SaveMetaDataAsync(_mapper.Map<MetaData>(model));
            if (item != null)
            {
                return _mapper.Map<MetaDataModel>(item);
            }

            return UnprocessableEntity();
        }

        /// <summary>
        /// Update MetaData
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
        public async Task<ActionResult> UpdateMetaDataAsync([FromForm] UpdateMetaDataModel model)
        {
            _logger.LogInformation("The method UpdateMetaDataAsync");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _metaDataManager.SaveMetaDataAsync(_mapper.Map<MetaData>(model));

            return Ok();
        }

        /// <summary>
        /// Remove MetaData
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
        public async Task<ActionResult> RemoveMetaDataAsync(string id)
        {
            _logger.LogInformation("The method RemoveMetaDataAsync");

            var entityId = new ObjectId(id);

            var item = await _metaDataManager.GetMetaDataByIdAsync(entityId);
            if (item == null)
            {
                return NotFound();
            }

            await _metaDataManager.RemoveMetaDataAsync(entityId);

            return Ok();
        }
    }
}