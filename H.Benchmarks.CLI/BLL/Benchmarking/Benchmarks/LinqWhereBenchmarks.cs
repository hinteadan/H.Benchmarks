using BenchmarkDotNet.Attributes;
using H.Necessaire;
using System.Collections.Generic;
using System.Linq;

namespace H.Benchmarks.CLI.BLL.Benchmarking.Benchmarks
{
    [ID("linq-where")]
    [Config(typeof(DefaultBenchmarksConfig))]
    public class LinqWhereBenchmarks : ImABenchmarkContainer
    {
        public string Name => "LINQ.Where() Benchmarks";

        public string Description => "Benchmarks for the .Where() extensions method. On IQueryable vs IEnumerable vs etc.";

        [Benchmark]
        [ArgumentsSource(nameof(NumbersDataSets))]
        public void LinqWhere_Over_IEnumerable(IEnumerable<int> numbers)
        {
            var result =
                numbers
                .Where(x => x > 0)
                .Where(x => x % 2 == 0)
                .Where(x => x % 5 == 0)
                .Where(x => x != 333)
                .ToArray()
                ;
        }

        [Benchmark]
        [ArgumentsSource(nameof(NumbersDataSets))]
        public void LinqWhere_Over_IEnumerable_With_Single_Big_Predicate(IEnumerable<int> numbers)
        {
            var result =
                numbers
                .Where(x => x > 0 && x > 0 && x > 0 && x > 0)
                .ToArray()
                ;
        }

        [Benchmark]
        [ArgumentsSource(nameof(NumbersDataSets))]
        public void LinqWhere_Over_IQueryable(IEnumerable<int> numbers)
        {
            var queryable = numbers.AsQueryable();

            var result =
                numbers
                .Where(x => x > 0)
                .Where(x => x % 2 == 0)
                .Where(x => x % 5 == 0)
                .Where(x => x != 333)
                .ToArray()
                ;
        }

        public IEnumerable<IEnumerable<int>> NumbersDataSets()
        {
            yield return GenerateNumbers(1000);
            yield return GenerateNumbers(10000);
            yield return GenerateNumbers(100000);
            yield return GenerateNumbers(1000000);
        }

        private IEnumerable<int> GenerateNumbers(int count)
        {
            return
                Enumerable
                .Range(0, count)
                ;
        }
    }
}
