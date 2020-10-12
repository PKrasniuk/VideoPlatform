using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.RequestModels
{
    /// <summary>
    /// AddPartnerModel
    /// </summary>
    public class AddPartnerModel
    {
        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty(propertyName: "name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty(propertyName: "description")]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        [JsonProperty(propertyName: "logo")]
        [Required]
        public string Logo { get; set; }

        /// <summary>
        /// ShowOnPartnerPage
        /// </summary>
        [JsonProperty(propertyName: "showOnPartnerPage")]
        [Required]
        [DefaultValue(true)]
        public bool ShowOnPartnerPage { get; set; }

        /// <summary>
        /// IsArchived
        /// </summary>
        [JsonProperty(propertyName: "isArchived")]
        [Required]
        [DefaultValue(false)]
        public bool IsArchived { get; set; }
    }
}