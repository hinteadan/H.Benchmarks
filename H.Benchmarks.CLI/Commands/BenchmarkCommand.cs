using BenchmarkDotNet.Running;
using H.Benchmarks.CLI.BLL;
using H.Necessaire;
using H.Necessaire.CLI.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace H.Benchmarks.CLI.Commands;

[ID("benchmark")]
[Alias("bench", "mark")]
internal class BenchmarkCommand : CommandBase
{
    Func<string, ImABenchmarkContainer> benchmarkClassFinder;
    public override void ReferDependencies(ImADependencyProvider dependencyProvider)
    {
        base.ReferDependencies(dependencyProvider);
        benchmarkClassFinder = id => dependencyProvider.Build<ImABenchmarkContainer>(id);
    }

    public override async Task<OperationResult> Run()
    {
        Log("Benchmarking...");
        using (new TimeMeasurement(x => Log($"DONE Benchmarking in {x}")))
        {
            Note[] args = (await GetArguments())?.Jump(1);

            if (args?.Any() != true)
                return await RunAllBenchmarks();

            return await RunSpecificBenchmark(args.First(), args.Jump(1));
        }
    }

    private Task<OperationResult> RunSpecificBenchmark(Note benchmarkArg, Note[] args)
    {
        string bechmarkID = benchmarkArg.ID;
        ImABenchmarkContainer benchmarkContainer = benchmarkClassFinder.Invoke(bechmarkID);

        if (benchmarkContainer is null)
            return OperationResult.Fail($"Benchmark {bechmarkID} doesn't exist").AsTask();

        BenchmarkRunner.Run(benchmarkContainer.GetType(), config: null, args: args.Select(x => x.ID).ToArray());

        return OperationResult.Win().AsTask();
    }

    private Task<OperationResult> RunAllBenchmarks()
    {
        var result = BenchmarkRunner.Run(typeof(BenchmarkCommand).Assembly);

        return OperationResult.Win().AsTask();
    }
}
