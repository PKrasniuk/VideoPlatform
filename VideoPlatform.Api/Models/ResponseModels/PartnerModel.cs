using System.ComponentModel;
using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.ResponseModels
{
    /// <summary>
    /// PartnerModel
    /// </summary>
    public class PartnerModel
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        [JsonProperty("logo")]
        public string Logo { get; set; }

        /// <summary>
        /// ShowOnPartnerPage
        /// </summary>
        [JsonProperty("showOnPartnerPage")]
        [DefaultValue(true)]
        public bool ShowOnPartnerPage { get; set; }

        /// <summary>
        /// IsArchived
        /// </summary>
        [JsonProperty("isArchived")]
        [DefaultValue(false)]
        public bool IsArchived { get; set; }
    }
}