using System;
using Microsoft.AspNetCore.Mvc;
using VideoPlatform.BLL.Interfaces;

namespace VideoPlatform.Api.Controllers;

/// <summary>
///     ExperimentsController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ExperimentsController : ControllerBase
{
    private readonly IExperimentManager _experimentManager;

    /// <summary>
    ///     ExperimentsController constructor
    /// </summary>
    /// <param name="experimentManager"></param>
    public ExperimentsController(IExperimentManager experimentManager)
    {
        _experimentManager = experimentManager ?? throw new ArgumentNullException(nameof(experimentManager));
    }
}