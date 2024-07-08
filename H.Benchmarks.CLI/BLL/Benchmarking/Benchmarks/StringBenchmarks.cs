using BenchmarkDotNet.Attributes;
using H.Necessaire;
using System;
using System.Linq;
using System.Text;

namespace H.Benchmarks.CLI.BLL.Benchmarking.Benchmarks;

[ID("strings")]
[Config(typeof(DefaultBenchmarksConfig))]
public class StringBenchmarks : ImABenchmarkContainer
{
    const int numberOfStringsToConcatenate = 100;
    static readonly string[] strings
        = Enumerable
        .Range(0, numberOfStringsToConcatenate)
        .Select(i => i.ToString())
        .ToArray()
        ;
    public string Name { get; } = "String Benchmarks";
    public string Description { get; } = "String operations benchmarks.";

    [Benchmark]
    public string SimpleStringAllocation()
    {
        return "Test, Test, Testicles";
    }

    [Benchmark]
    public string StringConcatenation_Via_Add_Operator()
    {
        string result = "";
        foreach (string s in strings)
        {
            result += s;
        }
        return result;
    }

    [Benchmark]
    public string StringConcatenation_Via_Interpolation()
    {
        string result = "";
        foreach (string s in strings)
        {
            result = $"{result}{s}";
        }
        return result;
    }

    [Benchmark]
    public string StringConcatenation_Via_StringBuilder()
    {
        StringBuilder resultBuilder = new StringBuilder();
        foreach (string s in strings)
        {
            resultBuilder.Append(s);
        }
        return resultBuilder.ToString();
    }

    [Benchmark]
    public string StringConcatenation_Via_String_Join()
    {
        return string.Join("", strings);
    }
}
