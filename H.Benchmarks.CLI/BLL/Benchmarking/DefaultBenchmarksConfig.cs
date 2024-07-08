using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Diagnostics.Windows;

namespace H.Benchmarks.CLI.BLL.Benchmarking;

internal class DefaultBenchmarksConfig : ManualConfig
{
    public DefaultBenchmarksConfig()
    {
        AddDiagnoser(
            MemoryDiagnoser.Default,
            new InliningDiagnoser(),
            ThreadingDiagnoser.Default,
            ExceptionDiagnoser.Default
        );
    }
}
