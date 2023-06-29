using System;
using Microsoft.AspNetCore.Mvc;
using VideoPlatform.BLL.Interfaces;

namespace VideoPlatform.Api.Controllers;

/// <summary>
///     ToolsController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ToolsController : ControllerBase
{
    private readonly IToolManager _toolManager;

    /// <summary>
    ///     ToolsController constructor
    /// </summary>
    /// <param name="toolManager"></param>
    public ToolsController(IToolManager toolManager)
    {
        _toolManager = toolManager ?? throw new ArgumentNullException(nameof(toolManager));
    }
}