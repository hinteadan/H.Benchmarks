using H.Necessaire;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace H.Benchmarks.CLI.BLL
{
    internal class CustomizableAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        int index = -1;
        readonly Func<T, int, Task<OperationResult<T>>> valueFactory;
        public CustomizableAsyncEnumerator(Func<T, int, Task<OperationResult<T>>> valueFactory)
        {
            this.valueFactory = valueFactory;
        }

        public T Current { get; private set; }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;

        public async ValueTask<bool> MoveNextAsync()
        {
            OperationResult<T> newValueResult = await valueFactory(Current, ++index);

            Current = newValueResult.Payload;

            return newValueResult.IsSuccessful;
        }
    }
}
