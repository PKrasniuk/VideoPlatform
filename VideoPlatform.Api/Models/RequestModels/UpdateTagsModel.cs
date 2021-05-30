using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.RequestModels
{
    /// <summary>
    /// UpdateTagsModel
    /// </summary>
    public class UpdateTagsModel
    {
        /// <summary>
        /// Tags
        /// </summary>
        [JsonProperty("tags")]
        [Required]
        public IList<UpdateTagModel> Tags { get; set; }
    }
}