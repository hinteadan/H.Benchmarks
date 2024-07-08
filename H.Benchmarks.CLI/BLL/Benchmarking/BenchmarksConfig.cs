using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Diagnostics.Windows;

namespace H.Benchmarks.CLI.BLL.Benchmarking;

internal class BenchmarksConfig : ManualConfig
{
    public BenchmarksConfig()
    {
        AddDiagnoser(
            MemoryDiagnoser.Default,
            new InliningDiagnoser(),
            new EtwProfiler(),
            ThreadingDiagnoser.Default,
            ExceptionDiagnoser.Default
        );
    }
}
