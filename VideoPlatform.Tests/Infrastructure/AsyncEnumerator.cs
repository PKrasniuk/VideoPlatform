using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace VideoPlatform.Tests.Infrastructure
{
    internal class AsyncEnumerator<T> : IAsyncEnumerator<T>, IDisposable
    {
        private readonly IEnumerator<T> _enumerator;

        public AsyncEnumerator(IEnumerator<T> enumerator) =>
            _enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));

        public ValueTask<bool> MoveNextAsync() => new(_enumerator.MoveNext());

        public Task<bool> MoveNext(CancellationToken cancellationToken) => Task.FromResult(_enumerator.MoveNext());

        public T Current => _enumerator.Current;

        public void Dispose()
        {
        }

        public async ValueTask DisposeAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            Dispose();
        }
    }
}