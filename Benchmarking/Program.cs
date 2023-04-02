using System;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;
using Benchmarking.Tests;

namespace Benchmarking
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ObjectToObjectTests>();
        }
    }
}
