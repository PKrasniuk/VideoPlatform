using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace VideoPlatform.Tests.Infrastructure
{
    internal class AsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> enumerator;

        public AsyncEnumerator(IEnumerator<T> enumerator) =>
            this.enumerator = enumerator ?? throw new ArgumentNullException();

        public ValueTask<bool> MoveNextAsync()
        {
            throw new NotImplementedException();
        }

        public T Current => enumerator.Current;

        public void Dispose()
        {
        }

        public Task<bool> MoveNext(CancellationToken cancellationToken) => Task.FromResult(enumerator.MoveNext());
        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
