using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoPlatform.Tests.Infrastructure
{
    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;
        private bool _disposed;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
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

        public async ValueTask DisposeAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            Dispose();
        }
    }
}