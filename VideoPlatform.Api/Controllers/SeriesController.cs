using System;
using Microsoft.AspNetCore.Mvc;
using VideoPlatform.BLL.Interfaces;

namespace VideoPlatform.Api.Controllers
{
    /// <summary>
    /// SeriesController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesManager _seriesManager;

        /// <summary>
        /// SeriesController constructor
        /// </summary>
        /// <param name="seriesManager"></param>
        public SeriesController(ISeriesManager seriesManager)
        {
            _seriesManager = seriesManager ?? throw new ArgumentNullException(nameof(seriesManager));
        }
    }
}