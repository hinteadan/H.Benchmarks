using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;

namespace H.Benchmarks.CLI.BLL.Benchmarking;

internal class DefaultBenchmarksConfig : ManualConfig
{
    public DefaultBenchmarksConfig()
    {
        AddDiagnoser(
            MemoryDiagnoser.Default
        );
    }
}
