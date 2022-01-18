using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace Benchmarking.Tests
{
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
