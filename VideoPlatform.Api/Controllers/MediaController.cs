using System;
using Microsoft.AspNetCore.Mvc;
using VideoPlatform.BLL.Interfaces;

namespace VideoPlatform.Api.Controllers
{
    /// <summary>
    /// MediaController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaManager _mediaManager;

        /// <summary>
        /// MediaController constructor
        /// </summary>
        /// <param name="mediaManager"></param>
        public MediaController(IMediaManager mediaManager)
        {
            _mediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
        }
    }
}