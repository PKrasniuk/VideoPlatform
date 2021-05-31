using System.Collections.Generic;
using System.Threading;
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
            {
                return;
            }

            if (disposing)
            {
                _inner?.Dispose();
            }

            _disposed = true;
        }

        public ValueTask<bool> MoveNextAsync()
        {
            throw new System.NotImplementedException();
        }

        public T Current => _inner.Current;

        public Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }

        public ValueTask DisposeAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
