using BenchmarkDotNet.Attributes;
using H.Necessaire;
using System;
using System.Collections.Generic;
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
    [ArgumentsSource(nameof(StringsToConcatenate))]
    public string StringConcatenation_Via_Add_Operator(string[] strings)
    {
        string result = "";
        foreach (string s in strings)
        {
            result += s;
        }
        return result;
    }

    [Benchmark]
    [ArgumentsSource(nameof(StringsToConcatenate))]
    public string StringConcatenation_Via_Interpolation(string[] strings)
    {
        string result = "";
        foreach (string s in strings)
        {
            result = $"{result}{s}";
        }
        return result;
    }

    [Benchmark]
    [ArgumentsSource(nameof(StringsToConcatenate))]
    public string StringConcatenation_Via_StringBuilder(string[] strings)
    {
        StringBuilder resultBuilder = new StringBuilder();
        foreach (string s in strings)
        {
            resultBuilder.Append(s);
        }
        return resultBuilder.ToString();
    }

    [Benchmark]
    [ArgumentsSource(nameof(StringsToConcatenate))]
    public string StringConcatenation_Via_String_Join(string[] strings)
    {
        return string.Join("", strings);
    }


    public IEnumerable<object[]> StringsToConcatenate()
    {
        yield return GenerateStrings(10);
        yield return GenerateStrings(100);
        yield return GenerateStrings(1000);
    }

    private string[] GenerateStrings(int numberOfStringsToConcatenate)
    {
        return
            Enumerable
            .Range(0, numberOfStringsToConcatenate)
            .Select(i => i.ToString())
            .ToArray()
            ;
    }
}
