using BenchmarkDotNet.Attributes;
using H.Necessaire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Benchmarks.CLI.BLL.Benchmarking.Benchmarks
{
    [ID("object-creation")]
    [Config(typeof(DefaultBenchmarksConfig))]
    public class ObjectCreationBenchmarks : ImABenchmarkContainer
    {
        public string Name => "Object Creation Benchmarks";

        public string Description => "Benchmarks for the object instantiation in .NET; new() vs Activator vs reflection constructor invoke";

        [Benchmark]
        [ArgumentsSource(nameof(NumberOfInstances))]
        public void DefaultWithNew(int numberOfInstances)
        {
            DummyData[] instances = new DummyData[numberOfInstances];
            for (int index = 0; index < numberOfInstances; index++)
            {
                instances[index] = new DummyData();
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(NumberOfInstances))]
        public void ViaActivatorCreateInstanceGeneric(int numberOfInstances)
        {
            DummyData[] instances = new DummyData[numberOfInstances];
            for (int index = 0; index < numberOfInstances; index++)
            {
                instances[index] = Activator.CreateInstance<DummyData>();
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(NumberOfInstances))]
        public void ViaActivatorCreateInstanceTypeof(int numberOfInstances)
        {
            DummyData[] instances = new DummyData[numberOfInstances];
            for (int index = 0; index < numberOfInstances; index++)
            {
                instances[index] = (DummyData)Activator.CreateInstance(typeof(DummyData));
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(NumberOfInstances))]
        public void ViaReflectionConstructorInvoke(int numberOfInstances)
        {
            var constructor = typeof(DummyData).GetConstructor([]);
            DummyData[] instances = new DummyData[numberOfInstances];
            for (int index = 0; index < numberOfInstances; index++)
            {
                instances[index] = (DummyData)constructor.Invoke([]);
            }
        }

        public IEnumerable<int> NumberOfInstances()
        {
            yield return 100;
            yield return 1000;
            yield return 10000;
            yield return 100000;
        }

        class DummyData : IGuidIdentity
        {
            public Guid ID { get; set; } = Guid.NewGuid();
            public string FirstName { get; set; } = Random.Shared.Next(0, 999999).ToString("D6");
            public string LastName { get; set; } = Random.Shared.Next(0, 999999).ToString("D6");
            public DateTime AsOf { get; set; } = DateTime.UtcNow;
            public Note[] Notes = Enumerable.Range(0, Random.Shared.Next(0, 5)).Select(i => Random.Shared.Next(0, 9999).ToString("D4").NoteAs($"Note{i}")).ToArrayNullIfEmpty();
        }
    }
}
