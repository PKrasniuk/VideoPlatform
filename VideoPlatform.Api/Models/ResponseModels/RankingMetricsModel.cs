using System.Collections.Generic;
using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.ResponseModels
{
    /// <summary>
    /// RankingMetricsModel
    /// </summary>
    public class RankingMetricsModel
    {
        /// <summary>
        /// NormalizedDiscountedCumulativeGains
        /// </summary>
        [JsonProperty("normalizedDiscountedCumulativeGains")]
        public IList<double> NormalizedDiscountedCumulativeGains { get; set; }

        /// <summary>
        /// DiscountedCumulativeGains
        /// </summary>
        [JsonProperty("discountedCumulativeGains")]
        public IList<double> DiscountedCumulativeGains { get; set; }
    }
}