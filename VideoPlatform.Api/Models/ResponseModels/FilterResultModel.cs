using System.Collections.Generic;
using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.ResponseModels
{
    /// <summary>
    /// FilterResultModel
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class FilterResultModel<TEntity>
    {
        /// <summary>
        /// Items
        /// </summary>
        [JsonProperty("items")]
        public ICollection<TEntity> Items { get; set; }

        /// <summary>
        /// TotalCount
        /// </summary>
        [JsonProperty("totalCount")]
        public long TotalCount { get; set; }
    }
}