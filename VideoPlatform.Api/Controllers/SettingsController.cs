using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoPlatform.Api.Models.RequestModels;
using VideoPlatform.Api.Models.ResponseModels;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.Common.Models.ResponseModels;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.Api.Controllers;

/// <summary>
///     Settings Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class SettingsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISettingManager _settingManager;

    /// <summary>
    ///     Settings Controller Constructor
    /// </summary>
    /// <param name="settingManager"></param>
    /// <param name="mapper"></param>
    public SettingsController(ISettingManager settingManager, IMapper mapper)
    {
        _settingManager = settingManager ?? throw new ArgumentNullException(nameof(settingManager));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    ///     Get Settings
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<SettingModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
    public async Task<ActionResult<ICollection<SettingModel>>> GetSettingsAsync()
    {
        var items = await _settingManager.GetSettingsCQRSAsync();
        return items.Any() ? _mapper.Map<Collection<SettingModel>>(items) : NotFound();
    }

    /// <summary>
    ///     Get Setting
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SettingModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
    public async Task<ActionResult<SettingModel>> GetSettingAsync(short id)
    {
        var item = await _settingManager.GetSettingCQRSAsync(id);
        return item != null ? _mapper.Map<SettingModel>(item) : NotFound();
    }

    /// <summary>
    ///     Add Setting
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SettingModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsModel))]
    public async Task<ActionResult<SettingModel>> AddSettingAsync([FromForm] AddSettingModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var item = await _settingManager.AddSettingCQRSAsync(_mapper.Map<Setting>(model));
        return item != null ? _mapper.Map<SettingModel>(item) : UnprocessableEntity();
    }

    /// <summary>
    ///     Update Setting
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
    public async Task<ActionResult> UpdateSettingAsync([FromForm] UpdateSettingModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _settingManager.UpdateSettingCQRSAsync(_mapper.Map<Setting>(model));

        return Ok();
    }

    /// <summary>
    ///     Remove Setting
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
    public async Task<ActionResult> RemoveSettingAsync(short id)
    {
        var item = await _settingManager.GetSettingCQRSAsync(id);
        if (item == null)
            return NotFound();

        await _settingManager.RemoveSettingCQRSAsync(id);

        return Ok();
    }
}