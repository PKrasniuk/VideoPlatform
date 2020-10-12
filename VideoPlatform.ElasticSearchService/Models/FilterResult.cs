using System.Collections.Generic;

namespace VideoPlatform.ElasticSearchService.Models
{
    public class FilterResult<TEntity>
    {
        public ICollection<TEntity> Items { get; set; }

        public long TotalCount { get; set; }
    }
}