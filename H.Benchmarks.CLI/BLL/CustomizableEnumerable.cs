using H.Necessaire;
using System;
using System.Collections;
using System.Collections.Generic;

namespace H.Benchmarks.CLI.BLL
{
    internal class CustomizableEnumerable<T> : IEnumerable<T>
    {
        readonly Func<T, int, OperationResult<T>> valueFactory;
        public CustomizableEnumerable(Func<T, int, OperationResult<T>> valueFactory)
        {
            this.valueFactory = valueFactory;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new CustomizableEnumerator<T>(valueFactory);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
