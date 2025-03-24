using H.Necessaire;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace H.Benchmarks.CLI.BLL
{
    internal static class DataGenerator
    {
        public static IEnumerable<int> NewRandomIntsEnumerable(int count = 10)
        {
            return new CustomizableEnumerable<int>((latestValue, index) => new OperationResult<int>
            {
                IsSuccessful = index < count,
                Payload = Random.Shared.Next(int.MinValue, int.MaxValue),
            });
        }

        public static IAsyncEnumerable<int> NewRandomIntsAsyncEnumerable(int count = 10)
        {
            return new CustomizableAsyncEnumerable<int>((latestValue, index) => new OperationResult<int>
            {
                IsSuccessful = index < count,
                Payload = Random.Shared.Next(int.MinValue, int.MaxValue),
            }.AsTask());
        }

        public static IEnumerable<T> NewRandomDataEnumerable<T>(Func<T> valueFactory, int count = 10)
        {
            return new CustomizableEnumerable<T>((latestValue, index) => new OperationResult<T>
            {
                IsSuccessful = index < count,
                Payload = valueFactory(),
            });
        }

        public static IAsyncEnumerable<T> NewRandomDataAsyncEnumerable<T>(Func<Task<T>> valueFactory, int count = 10)
        {
            return new CustomizableAsyncEnumerable<T>(async (latestValue, index) => new OperationResult<T>
            {
                IsSuccessful = index < count,
                Payload = await valueFactory(),
            });
        }
    }
}
