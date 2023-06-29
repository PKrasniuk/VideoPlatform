using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.RequestModels;

/// <summary>
///     InputTripModel
/// </summary>
public class InputTripModel
{
    /// <summary>
    ///     VendorId
    /// </summary>
    [JsonProperty("vendorId")]
    [Required]
    public string VendorId { get; set; }

    /// <summary>
    ///     RateCode
    /// </summary>
    [JsonProperty("rateCode")]
    [Required]
    public string RateCode { get; set; }

    /// <summary>
    ///     PassengerCount
    /// </summary>
    [JsonProperty("passengerCount")]
    [Required]
    public float PassengerCount { get; set; }

    /// <summary>
    ///     TripTime
    /// </summary>
    [JsonProperty("tripTime")]
    [Required]
    public float TripTime { get; set; }

    /// <summary>
    ///     TripDistance
    /// </summary>
    [JsonProperty("tripDistance")]
    [Required]
    public float TripDistance { get; set; }

    /// <summary>
    ///     PaymentType
    /// </summary>
    [JsonProperty("paymentType")]
    [Required]
    public string PaymentType { get; set; }

    /// <summary>
    ///     FareAmount
    /// </summary>
    [JsonProperty("fareAmount")]
    [Required]
    public float FareAmount { get; set; }

    /// <summary>
    ///     RebuildModel
    /// </summary>
    [JsonProperty("rebuildModel")]
    [Required]
    [DefaultValue(false)]
    public bool RebuildModel { get; set; }
}