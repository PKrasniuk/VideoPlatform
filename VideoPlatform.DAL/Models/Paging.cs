using System;
using System.Linq.Expressions;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Common.Models.Enums;

namespace VideoPlatform.DAL.Models
{
    public class Paging<TEntity>
    {
        public int PageNumber { get; set; } = FilterConstants.DefaultPageNumber;

        public int PageSize { get; set; } = FilterConstants.DefaultPageSize;

        public Expression<Func<TEntity, dynamic>> SortedProperty { get; set; }

        public SortOrder SortOrder { get; set; } = SortOrder.Ascending;

        public Expression<Func<TEntity, bool>> FilterExpression { get; set; }
    }
}