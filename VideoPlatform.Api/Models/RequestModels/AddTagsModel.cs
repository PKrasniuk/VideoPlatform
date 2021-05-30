using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.RequestModels
{
    /// <summary>
    /// AddTagsModel
    /// </summary>
    public class AddTagsModel
    {
        /// <summary>
        /// Tags
        /// </summary>
        [JsonProperty("tags")]
        [Required]
        public IList<AddTagModel> Tags { get; set; }
    }
}