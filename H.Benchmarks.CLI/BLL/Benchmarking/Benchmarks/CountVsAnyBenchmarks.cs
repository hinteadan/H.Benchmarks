using BenchmarkDotNet.Attributes;
using H.Necessaire;
using System.Collections.Generic;
using System.Linq;

namespace H.Benchmarks.CLI.BLL.Benchmarking.Benchmarks
{
    [ID("count-vs-any")]
    [Config(typeof(DefaultBenchmarksConfig))]
    public class CountVsAnyBenchmarks : ImABenchmarkContainer
    {
        public string Name => "CountVsAny";

        public string Description => "Benchmark .Count or .Length vs .Any() performance";

        [Benchmark]
        [ArgumentsSource(nameof(NumbersLists))]
        public void CountOnList(List<int> data)
        {
            bool isNotEmpty = data.Count > 0;
        }

        [Benchmark]
        [ArgumentsSource(nameof(NumbersLists))]
        public void AnyOnList(List<int> data)
        {
            bool isNotEmpty = data.Any();
        }

        [Benchmark]
        [ArgumentsSource(nameof(NumbersEnumerables))]
        public void AnyOnEnumerable(IEnumerable<int> data)
        {
            bool isNotEmpty = data.Any();
        }

        public IEnumerable<List<int>> NumbersLists()
        {
            yield return DataGenerator.NewRandomIntsEnumerable(1000).ToList();
            yield return DataGenerator.NewRandomIntsEnumerable(10000).ToList();
            yield return DataGenerator.NewRandomIntsEnumerable(100000).ToList();
            yield return DataGenerator.NewRandomIntsEnumerable(1000000).ToList();
        }

        public IEnumerable<IEnumerable<int>> NumbersEnumerables()
        {
            yield return DataGenerator.NewRandomIntsEnumerable(1000);
            yield return DataGenerator.NewRandomIntsEnumerable(10000);
            yield return DataGenerator.NewRandomIntsEnumerable(100000);
            yield return DataGenerator.NewRandomIntsEnumerable(1000000);
        }
    }
}
