using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace Benchmarking.Tests
{
    //|              Method |      Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
    //|-------------------- |----------:|---------:|---------:|-------:|------:|------:|----------:|
    //| IEnumurableConsumer | 203.69 ns | 4.083 ns | 8.875 ns | 0.0050 |     - |     - |      32 B |
    //|       ArrayConsumer |  49.38 ns | 1.018 ns | 2.478 ns | 0.0357 |     - |     - |     224 B |
    //|        ListConsumer | 251.66 ns | 5.034 ns | 6.720 ns | 0.1030 |     - |     - |     648 B |

    [MemoryDiagnoser]
    public  class IEnumurableTests
    {
        private const int NumberOfElements = 50;

        [Benchmark]
        public void IEnumurableConsumer()
        {
            foreach (var item in IEnumurable())
            {
                
            }
        }
        
        public IEnumerable<int> IEnumurable()
        {
            for (int i = 0; i < NumberOfElements; i++)
            {
                yield return i;
            }
        }

        [Benchmark]
        public void ArrayConsumer()
        {
            foreach (var item in Array())
            {

            }
        }

        public int[] Array()
        {
            var result = new int[NumberOfElements];
            for (int i = 0; i < NumberOfElements; i++)
            {
                result[i] = i;
            }

            return result;
        }

        [Benchmark]
        public void ListConsumer()
        {
            foreach (var item in List())
            {

            }
        }
        public List<int> List()
        {
            var result = new List<int>();
            for (int i = 0; i < NumberOfElements; i++)
            {
                result.Add(i);
            }

            return result;
        }
    }

}
