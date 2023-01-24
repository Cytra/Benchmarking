using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace Benchmarking.Tests
{

    //|          Method |     Mean |     Error |    StdDev |   Median |
    //|---------------- |---------:|----------:|----------:|---------:|
    //|   NormalForEach | 1.143 ms | 0.0630 ms | 0.1858 ms | 1.201 ms |
    //| ParallelForEach | 1.426 ms | 0.0544 ms | 0.1605 ms | 1.475 ms |
    
    //[PerfCollectProfiler(performExtraBenchmarksRun: true)]
    public class ParallelBenchmarking
    {
        [Benchmark]
        public int[] NormalForEach()
        {
            var array = new int[1_000_000];
            for (var i = 0; i < 1_000_000; i++)
            {
                array[i] = i;
            }
            return array;
        }

        [Benchmark]
        public int[] ParallelForEach()
        {
            var array = new int[1_000_000];
            Parallel.For(0, 1_000_000, i =>
            {
                array[i] = i;
            });
            return array;
        }
    }
}
