using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace VideoPlatform.NotificationService.Models.RequestModels
{
    /// <summary>
    /// NotificationModel
    /// </summary>
    public class NotificationModel
    {
        /// <summary>
        /// Key
        /// </summary>
        [JsonProperty("key")]
        [Required]
        public string Key { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        [JsonProperty("message")]
        [Required]
        public string Message { get; set; }
    }
}