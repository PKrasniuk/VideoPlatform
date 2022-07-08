using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoPlatform.Tests.Infrastructure
{
    internal sealed class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        private bool _disposed;

        public TestAsyncEnumerator(IEnumerator<T> inner) =>
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));

        public void Dispose() => Dispose(true);

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing) 
                _inner?.Dispose();

            _disposed = true;
        }

        public Task<bool> MoveNext() => Task.FromResult(_inner.MoveNext());

        public ValueTask<bool> MoveNextAsync() => new(_inner.MoveNext());

        public T Current => _inner.Current;

        public ValueTask DisposeAsync()
        {
            _inner.Dispose();

            return new ValueTask(Task.CompletedTask);
        }
    }
}