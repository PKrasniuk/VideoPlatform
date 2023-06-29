using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace VideoPlatform.Tests.Infrastructure;

internal class AsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>
{
    public AsyncEnumerable(Expression expression) : base(expression)
    {
    }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return GetEnumerator();
    }

    public IAsyncEnumerator<T> GetEnumerator()
    {
        return new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }
}