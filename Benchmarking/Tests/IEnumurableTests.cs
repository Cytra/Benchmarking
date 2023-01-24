using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace Benchmarking.Tests;

//|              Method |      Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
//|-------------------- |----------:|---------:|---------:|-------:|------:|------:|----------:|
//| IEnumurableConsumer | 203.69 ns | 4.083 ns | 8.875 ns | 0.0050 |     - |     - |      32 B |
//|       ArrayConsumer |  49.38 ns | 1.018 ns | 2.478 ns | 0.0357 |     - |     - |     224 B |
//|        ListConsumer | 251.66 ns | 5.034 ns | 6.720 ns | 0.1030 |     - |     - |     648 B |

[MemoryDiagnoser]
public class IEnumurableTests
{
    private const int ListLength = 1000;
    [Benchmark]
    public void IEnumurableConsumer()
    {
        var total = 0;
        foreach (var item in IEnumurable()) total += item;
    }

    public IEnumerable<int> IEnumurable()
    {
        for (var i = 0; i < ListLength; i++) yield return i;
    }

    [Benchmark]
    public void ArrayConsumer()
    {
        var total = 0;
        foreach (var item in Array()) total += item;
    }

    [Benchmark]
    public void ArraySpanConsumer()
    {
        int data = 0;
        Span<int> stackSpan = stackalloc int[ListLength*2];
        for (int ctr = 0; ctr < stackSpan.Length; ctr++)
            stackSpan[ctr] = data++;

        int stackSum = 0;
        foreach (var value in stackSpan)
            stackSum += value;

        //Span<int> array = stackalloc int[ListLength];
        //for (var i = 0; i < ListLength; i++) array[i] = i;

        //var total = 0;
        //foreach (var item in array) 
        //    total += item;
    }

    [Benchmark]
    public void ChatGPTSuggestion()
    {
        var array = ParallelForEach();
        var total = 0;

        Parallel.For(0, array.Length, x => { total += x; });
    }

    public int[] ParallelForEach()
    {
        var array = new int[ListLength];
        Parallel.For(0, ListLength, i => { array[i] = i; });
        return array;
    }

    public int[] Array()
    {
        var result = new int[ListLength];
        for (var i = 0; i < ListLength; i++) result[i] = i;

        return result;
    }

    [Benchmark]
    public void ListConsumer()
    {
        var total = 0;
        foreach (var item in List()) total += item;
    }

    public List<int> List()
    {
        var result = new List<int>();
        for (var i = 0; i < ListLength; i++) result.Add(i);

        return result;
    }
}