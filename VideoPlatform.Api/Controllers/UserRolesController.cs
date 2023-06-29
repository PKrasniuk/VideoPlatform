using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoPlatform.Api.Models.ResponseModels;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.Common.Models.ResponseModels;

namespace VideoPlatform.Api.Controllers;

/// <summary>
///     UserRoles Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UserRolesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserRolesManager _userRolesManager;

    /// <summary>
    ///     UserRoles Controller Constructor
    /// </summary>
    /// <param name="userRolesManager"></param>
    /// <param name="mapper"></param>
    public UserRolesController(IUserRolesManager userRolesManager, IMapper mapper)
    {
        _userRolesManager = userRolesManager ?? throw new ArgumentNullException(nameof(userRolesManager));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    ///     Get UserRoles
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<UserRoleModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
    public async Task<ActionResult<ICollection<UserRoleModel>>> GetUserRolesAsync()
    {
        var result = await _userRolesManager.GetUserRolesAsync();
        return result != null && result.Any()
            ? result.Select(x => _mapper.Map<UserRoleModel>(x)).ToList()
            : NotFound();
    }

    /// <summary>
    ///     Get UserRoles Alternative
    /// </summary>
    /// <returns></returns>
    [HttpGet("alternative")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<UserRoleModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsModel))]
    public async Task<ActionResult<ICollection<UserRoleModel>>> GetUserRolesAlternativeAsync()
    {
        var result = await _userRolesManager.GetUserRolesAlternativeAsync();
        return result != null && result.Any()
            ? result.Select(x => _mapper.Map<UserRoleModel>(x)).ToList()
            : NotFound();
    }
}