using System.Collections.Generic;

namespace VideoPlatform.DAL.Models;

public class PagingResult<TEntity>
{
    public ICollection<TEntity> Items { get; set; }

    public long TotalCount { get; set; }
}