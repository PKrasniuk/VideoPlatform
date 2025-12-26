using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoPlatform.Tests.Infrastructure;

internal sealed class TestAsyncEnumerator<T>(IEnumerator<T> inner) : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> _inner = inner ?? throw new ArgumentNullException(nameof(inner));

    private bool _disposed;

    public ValueTask<bool> MoveNextAsync()
    {
        return new ValueTask<bool>(_inner.MoveNext());
    }

    public T Current => _inner.Current;

    public ValueTask DisposeAsync()
    {
        _inner.Dispose();

        return new ValueTask(Task.CompletedTask);
    }

    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
            _inner?.Dispose();

        _disposed = true;
    }

    public Task<bool> MoveNext()
    {
        return Task.FromResult(_inner.MoveNext());
    }
}