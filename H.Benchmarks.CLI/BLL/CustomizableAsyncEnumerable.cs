using H.Necessaire;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace H.Benchmarks.CLI.BLL
{
    internal class CustomizableAsyncEnumerable<T> : IAsyncEnumerable<T>
    {
        readonly Func<T, int, Task<OperationResult<T>>> valueFactory;
        public CustomizableAsyncEnumerable(Func<T, int, Task<OperationResult<T>>> valueFactory)
        {
            this.valueFactory = valueFactory;
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new CustomizableAsyncEnumerator<T>(valueFactory);
        }
    }
}
