using System;
using BenchmarkDotNet.Attributes;

namespace Benchmarking.Tests;

[MemoryDiagnoser]
public class ReadOnlySpanBenchmark
{
    [Benchmark]
    public (int day, int month, int year) GetDateFromString()
    {
        var date = "02 01 2022";
        var dayString = date.Substring(0, 2);
        var monthString = date.Substring(3, 2);
        var yearString = date.Substring(6, 2);
        var day = int.Parse(dayString);
        var month = int.Parse(monthString);
        var year = int.Parse(yearString);
        return (day, month, year);
    }

    [Benchmark]
    public (int day, int month, int year) GetDateFromStringChartGpt()
    {
        var date = "02 01 2022";
        var dateStringArray = date.Split();
        var day = int.Parse(dateStringArray[0]);
        var month = int.Parse(dateStringArray[1]);
        var year = int.Parse(dateStringArray[2]);
        return (day, month, year);
    }

    [Benchmark]
    public (int day, int month, int year) GetDateFromStringReadOnlySpan()
    {
        ReadOnlySpan<char> date = "02 01 2022";
        var dayString = date.Slice(0, 2);
        var monthString = date.Slice(3, 2);
        var yearString = date.Slice(6, 2);
        var day = int.Parse(dayString);
        var month = int.Parse(monthString);
        var year = int.Parse(yearString);
        return (day, month, year);
    }
}