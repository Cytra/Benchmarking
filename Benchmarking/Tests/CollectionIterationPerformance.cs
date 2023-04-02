using System.Collections.Generic;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace Benchmarking.Tests;

[MemoryDiagnoser]
public class CollectionIterationPerformance
{
    private const int CollectionLength = 1000;
    private readonly IEnumerable<int> _enumerable;
    private readonly int[] _array;
    private readonly List<int> _list;
    private readonly HashSet<int> _hashSet;
    private readonly SortedSet<int> _sorterSet;

    public CollectionIterationPerformance()
    {
        _enumerable = GetEnumerable();
        _array = GetArray();
        _list = GetList();
        _hashSet = GetHashSet();
        _sorterSet = GetSortedSet();
    }

    private IEnumerable<int> GetEnumerable()
    {
        for (var i = 0; i < CollectionLength; i++) yield return i;
    }

    [Benchmark]
    public void EnumerableConsumer()
    {
        var total = 0;
        foreach (var item in _enumerable) total += item;
    }

    private HashSet<int> GetHashSet()
    {
        var result = new HashSet<int>();
        for (var i = 0; i < CollectionLength; i++)
            result.Add(i);
        return result;
    }

    [Benchmark]
    public void HashSetConsumer()
    {
        var total = 0;
        foreach (var item in _hashSet) total += item;
    }

    public int[] GetArray()
    {
        var result = new int[CollectionLength];
        for (var i = 0; i < CollectionLength; i++) result[i] = i;

        return result;
    }

    [Benchmark]
    public void ArrayConsumer()
    {
        var total = 0;
        foreach (var item in _array) total += item;
    }

    //[Benchmark]
    //public void ArraySpanConsumerWithGeneration()
    //{
    //    int data = 0;
    //    Span<int> stackSpan = stackalloc int[CollectionLength * 2];
    //    for (int ctr = 0; ctr < stackSpan.Length; ctr++)
    //        stackSpan[ctr] = data++;

    //    int stackSum = 0;
    //    foreach (var value in stackSpan)
    //        stackSum += value;
    //}

    [Benchmark]
    public void ParallelForConsumer()
    {
        var total = 0;
        Parallel.For(0, _array.Length, x => { total += x; });
    }

    public List<int> GetList()
    {
        var result = new List<int>();
        for (var i = 0; i < CollectionLength; i++) result.Add(i);

        return result;
    }

    [Benchmark]
    public void ListConsumer()
    {
        var total = 0;
        foreach (var item in _list) total += item;
    }

    private SortedSet<int> GetSortedSet()
    {
        var result = new SortedSet<int>();
        for (var i = 0; i < CollectionLength; i++) result.Add(i);

        return result;
    }

    [Benchmark]
    public void SortedSetConsumer()
    {
        var total = 0;
        foreach (var item in _sorterSet) total += item;
    }
}