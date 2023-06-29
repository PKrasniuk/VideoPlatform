using System;
using Microsoft.AspNetCore.Mvc;
using VideoPlatform.BLL.Interfaces;

namespace VideoPlatform.Api.Controllers;

/// <summary>
///     MediaTagsController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class MediaTagsController : ControllerBase
{
    private readonly IMediaTagManager _mediaTagsManager;

    /// <summary>
    ///     MediaTagsController constructor
    /// </summary>
    /// <param name="mediaTagsManager"></param>
    public MediaTagsController(IMediaTagManager mediaTagsManager)
    {
        _mediaTagsManager = mediaTagsManager ?? throw new ArgumentNullException(nameof(mediaTagsManager));
    }
}