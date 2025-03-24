using H.Necessaire;
using System;
using System.Collections;
using System.Collections.Generic;

namespace H.Benchmarks.CLI.BLL
{
    internal class CustomizableEnumerator<T> : IEnumerator<T>
    {
        int index = -1;
        readonly Func<T, int, OperationResult<T>> valueFactory;
        public CustomizableEnumerator(Func<T, int, OperationResult<T>> valueFactory)
        {
            this.valueFactory = valueFactory;
        }

        public T Current { get; private set; }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            OperationResult<T> newValueResult = valueFactory(Current, ++index);

            Current = newValueResult.Payload;

            return newValueResult.IsSuccessful;
        }

        public void Dispose() { }

        public void Reset() { }
    }
}
