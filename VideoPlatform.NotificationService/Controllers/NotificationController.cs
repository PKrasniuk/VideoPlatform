using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using VideoPlatform.Common.Models.ResponseModels;
using VideoPlatform.NotificationService.Hubs;
using VideoPlatform.NotificationService.Models.RequestModels;

namespace VideoPlatform.NotificationService.Controllers
{
    /// <summary>
    /// NotificationController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hub;

        /// <summary>
        /// NotificationController constructor
        /// </summary>
        /// <param name="hub"></param>
        public NotificationController(IHubContext<NotificationHub> hub)
        {
            _hub = hub ?? throw new ArgumentNullException(nameof(hub));
        }

        /// <summary>
        /// Notify
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("notify")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotificationModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailsModel))]
        public async Task<ActionResult> NotifyAsync([FromForm] NotificationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _hub.Clients.All.SendAsync(model.Key, model.Message);

            return Ok();
        }
    }
}