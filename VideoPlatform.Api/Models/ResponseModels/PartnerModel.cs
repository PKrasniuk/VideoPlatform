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
        [JsonProperty(propertyName: "id")]
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty(propertyName: "name")]
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty(propertyName: "description")]
        public string Description { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        [JsonProperty(propertyName: "logo")]
        public string Logo { get; set; }

        /// <summary>
        /// ShowOnPartnerPage
        /// </summary>
        [JsonProperty(propertyName: "showOnPartnerPage")]
        [DefaultValue(true)]
        public bool ShowOnPartnerPage { get; set; }

        /// <summary>
        /// IsArchived
        /// </summary>
        [JsonProperty(propertyName: "isArchived")]
        [DefaultValue(false)]
        public bool IsArchived { get; set; }
    }
}