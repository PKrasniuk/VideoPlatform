using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace VideoPlatform.Tests.Infrastructure;

internal class AsyncEnumerable<T>(Expression expression) : EnumerableQuery<T>(expression), IAsyncEnumerable<T>
{
    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return GetEnumerator();
    }

    public IAsyncEnumerator<T> GetEnumerator()
    {
        return new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }
}