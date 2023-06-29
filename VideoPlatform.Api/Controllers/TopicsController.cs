using System;
using Microsoft.AspNetCore.Mvc;
using VideoPlatform.BLL.Interfaces;

namespace VideoPlatform.Api.Controllers;

/// <summary>
///     TopicsController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TopicsController : ControllerBase
{
    private readonly ITopicManager _topicManager;

    /// <summary>
    ///     TopicsController
    /// </summary>
    /// <param name="topicManager"></param>
    public TopicsController(ITopicManager topicManager)
    {
        _topicManager = topicManager ?? throw new ArgumentNullException(nameof(topicManager));
    }
}