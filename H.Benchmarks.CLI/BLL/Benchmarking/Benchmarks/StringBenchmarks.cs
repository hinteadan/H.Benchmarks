using BenchmarkDotNet.Attributes;
using H.Necessaire;

namespace H.Benchmarks.CLI.BLL.Benchmarking.Benchmarks;

[ID("strings")]
[Config(typeof(DefaultBenchmarksConfig))]
public class StringBenchmarks : ImABenchmarkContainer
{
    public string Name { get; } = "String Benchmarks";

    public string Description { get; } = "String operations benchmarks.";

    [Benchmark]
    public string SimpleStringAllocation()
    {
        return "Test, Test, Testicles";
    }
}
