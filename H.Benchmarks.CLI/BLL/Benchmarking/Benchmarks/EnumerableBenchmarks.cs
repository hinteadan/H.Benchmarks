using BenchmarkDotNet.Attributes;
using H.Necessaire;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace H.Benchmarks.CLI.BLL.Benchmarking.Benchmarks
{
    [ID("enumerables")]
    [Config(typeof(DefaultBenchmarksConfig))]
    public class EnumerableBenchmarks : ImABenchmarkContainer
    {
        public string Name => "Enumerables";

        public string Description => "Various C# data streams benchmarks";

        [Benchmark]
        [ArgumentsSource(nameof(SyncNumbersDataStream))]
        public void Iterate_IEnumerable_IntStream(IEnumerable<int> dataStream)
        {
            foreach (int data in dataStream) { }
        }

        [Benchmark]
        [ArgumentsSource(nameof(AsyncNumbersDataStream))]
        public async Task Iterate_IAsyncEnumerable_IntStream(IAsyncEnumerable<int> dataStream)
        {
            await foreach (int data in dataStream) { }
        }

        [Benchmark]
        [ArgumentsSource(nameof(SyncDataStream))]
        public void Iterate_IEnumerable_DataStream(IEnumerable<DummyData> dataStream)
        {
            foreach (DummyData data in dataStream) { }
        }

        [Benchmark]
        [ArgumentsSource(nameof(AsyncDataStream))]
        public async Task Iterate_IAsyncEnumerable_DataStream(IAsyncEnumerable<DummyData> dataStream)
        {
            await foreach (DummyData data in dataStream) { }
        }

        public IEnumerable<IEnumerable<int>> SyncNumbersDataStream()
        {
            yield return DataGenerator.NewRandomIntsEnumerable(1000);
            yield return DataGenerator.NewRandomIntsEnumerable(10000);
            yield return DataGenerator.NewRandomIntsEnumerable(100000);
            yield return DataGenerator.NewRandomIntsEnumerable(1000000);
        }

        public IEnumerable<IAsyncEnumerable<int>> AsyncNumbersDataStream()
        {
            yield return DataGenerator.NewRandomIntsAsyncEnumerable(1000);
            yield return DataGenerator.NewRandomIntsAsyncEnumerable(10000);
            yield return DataGenerator.NewRandomIntsAsyncEnumerable(100000);
            yield return DataGenerator.NewRandomIntsAsyncEnumerable(1000000);
        }

        public IEnumerable<IEnumerable<DummyData>> SyncDataStream()
        {
            yield return DataGenerator.NewRandomDataEnumerable(GetDummyDataSync, 1000);
            yield return DataGenerator.NewRandomDataEnumerable(GetDummyDataSync, 10000);
            yield return DataGenerator.NewRandomDataEnumerable(GetDummyDataSync, 100000);
            yield return DataGenerator.NewRandomDataEnumerable(GetDummyDataSync, 1000000);
        }

        public IEnumerable<IAsyncEnumerable<DummyData>> AsyncDataStream()
        {
            yield return DataGenerator.NewRandomDataAsyncEnumerable(GetDummyData, 1000);
            yield return DataGenerator.NewRandomDataAsyncEnumerable(GetDummyData, 10000);
            yield return DataGenerator.NewRandomDataAsyncEnumerable(GetDummyData, 100000);
            yield return DataGenerator.NewRandomDataAsyncEnumerable(GetDummyData, 1000000);
        }

        static async Task<DummyData> GetDummyData() => await Task.Run(() => new DummyData()).ConfigureAwait(continueOnCapturedContext: false);

        static DummyData GetDummyDataSync() => GetDummyData().GetAwaiter().GetResult();

        public class DummyData : IGuidIdentity
        {
            public Guid ID { get; set; } = Guid.NewGuid();
            public DateTime AsOf { get; set; } = DateTime.UtcNow;
            public string Name { get; set; } = Guid.NewGuid().ToString();
            public int Version { get; set; } = Random.Shared.Next(int.MinValue, int.MaxValue);
        }
    }
}
