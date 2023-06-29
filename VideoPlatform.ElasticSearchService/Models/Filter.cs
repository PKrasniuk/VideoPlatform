using System;
using System.Linq.Expressions;
using Nest;
using VideoPlatform.Common.Infrastructure.Constants;

namespace VideoPlatform.ElasticSearchService.Models;

public class Filter<TEntity>
{
    public int PageNumber { get; set; } = FilterConstants.DefaultPageNumber;

    public int PageSize { get; set; } = FilterConstants.DefaultPageSize;

    public Expression<Func<TEntity, dynamic>> SortedProperty { get; set; }

    public SortOrder SortOrder { get; set; } = SortOrder.Ascending;

    public string FilterQuery { get; set; }
}