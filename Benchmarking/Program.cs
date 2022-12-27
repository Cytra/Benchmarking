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
            int[] numbers = { 1, 2, 3, 4, };

            var evenNumbers = from n in numbers
                where n % 2 == 0
                select n;

            foreach (int number in evenNumbers)
            {
                Console.WriteLine(number);
            }

            var counterValue = Counter.Count;

            var summary = BenchmarkRunner.Run<IEnumurableTests>();
        }
    }

    public class Counter
    {
        public static int Count = 10;
    }

}
