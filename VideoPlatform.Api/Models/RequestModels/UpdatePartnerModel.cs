﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.RequestModels
{
    /// <summary>
    /// UpdatePartnerModel
    /// </summary>
    public class UpdatePartnerModel
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("description")]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        [JsonProperty("logo")]
        [Required]
        public string Logo { get; set; }

        /// <summary>
        /// ShowOnPartnerPage
        /// </summary>
        [JsonProperty("showOnPartnerPage")]
        [Required]
        [DefaultValue(true)]
        public bool ShowOnPartnerPage { get; set; }

        /// <summary>
        /// IsArchived
        /// </summary>
        [JsonProperty("isArchived")]
        [Required]
        [DefaultValue(false)]
        public bool IsArchived { get; set; }
    }
}