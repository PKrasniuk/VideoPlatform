using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.RequestModels
{
    /// <summary>
    /// InputTripModel
    /// </summary>
    public class InputTripModel
    {
        /// <summary>
        /// VendorId
        /// </summary>
        [JsonProperty(propertyName: "vendorId")]
        [Required]
        public string VendorId { get; set; }

        /// <summary>
        /// RateCode
        /// </summary>
        [JsonProperty(propertyName: "rateCode")]
        [Required]
        public string RateCode { get; set; }

        /// <summary>
        /// PassengerCount
        /// </summary>
        [JsonProperty(propertyName: "passengerCount")]
        [Required]
        public float PassengerCount { get; set; }

        /// <summary>
        /// TripTime
        /// </summary>
        [JsonProperty(propertyName: "tripTime")]
        [Required]
        public float TripTime { get; set; }

        /// <summary>
        /// TripDistance
        /// </summary>
        [JsonProperty(propertyName: "tripDistance")]
        [Required]
        public float TripDistance { get; set; }

        /// <summary>
        /// PaymentType
        /// </summary>
        [JsonProperty(propertyName: "paymentType")]
        [Required]
        public string PaymentType { get; set; }

        /// <summary>
        /// FareAmount
        /// </summary>
        [JsonProperty(propertyName: "fareAmount")]
        [Required]
        public float FareAmount { get; set; }

        /// <summary>
        /// RebuildModel
        /// </summary>
        [JsonProperty(propertyName: "rebuildModel")]
        [Required]
        [DefaultValue(false)]
        public bool RebuildModel { get; set; }
    }
}