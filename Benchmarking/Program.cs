using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Benchmarking
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ValueTaskBenchmark>();
            //var valueTaskBench = new ValueTaskBenchmark();
            //await valueTaskBench.GetProfileTask();
            //await valueTaskBench.GetProfileTask();
        }
    }

}
