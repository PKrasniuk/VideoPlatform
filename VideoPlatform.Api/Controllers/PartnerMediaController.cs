using System;
using Microsoft.AspNetCore.Mvc;
using VideoPlatform.BLL.Interfaces;

namespace VideoPlatform.Api.Controllers
{
    /// <summary>
    /// PartnerMediaController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerMediaController : ControllerBase
    {
        private readonly IPartnerMediaManager _partnerMediaManager;

        /// <summary>
        /// PartnerMediaController constructor
        /// </summary>
        /// <param name="partnerMediaManager"></param>
        public PartnerMediaController(IPartnerMediaManager partnerMediaManager)
        {
            _partnerMediaManager = partnerMediaManager ?? throw new ArgumentNullException(nameof(partnerMediaManager));
        }
    }
}