using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace VideoPlatform.Tests.Infrastructure;

internal class AsyncEnumerator<T>(IEnumerator<T> enumerator) : IAsyncEnumerator<T>, IDisposable
{
    private readonly IEnumerator<T> _enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));

    public ValueTask<bool> MoveNextAsync()
    {
        return new ValueTask<bool>(_enumerator.MoveNext());
    }

    public T Current => _enumerator.Current;

    public async ValueTask DisposeAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(1));

        Dispose();
    }

    public void Dispose()
    {
    }

    public Task<bool> MoveNext(CancellationToken cancellationToken)
    {
        return Task.FromResult(_enumerator.MoveNext());
    }
}