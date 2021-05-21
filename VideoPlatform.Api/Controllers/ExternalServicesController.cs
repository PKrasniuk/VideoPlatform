using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VideoPlatform.Common.Infrastructure.Constants;

namespace VideoPlatform.Api.Controllers
{
    /// <summary>
    /// ExternalServicesController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalServicesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ExternalServicesController> _logger;

        /// <summary>
        /// ExternalServicesController constructor
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="logger"></param>
        public ExternalServicesController(IHttpClientFactory httpClientFactory, ILogger<ExternalServicesController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        /// <summary>
        /// GitHub Api
        /// </summary>
        /// <returns></returns>
        [HttpGet("gitHubApi")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<ActionResult<string>> GitHubApiAsync()
        {
            _logger.LogInformation("The method GitHubApiAsync");

            var httpClient = _httpClientFactory.CreateClient(ExternalServiceConstants.GitHubService);
            var httpResponseMessage = await httpClient.GetAsync("repos/symfony/symfony/contributors");

            return httpResponseMessage.IsSuccessStatusCode
                ? Ok(await httpResponseMessage.Content.ReadAsStringAsync())
                : StatusCode((int) httpResponseMessage.StatusCode,
                    await httpResponseMessage.Content.ReadAsStringAsync());
        }
    }
}