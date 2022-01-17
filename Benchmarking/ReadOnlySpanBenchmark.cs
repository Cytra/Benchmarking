using System;
using BenchmarkDotNet.Attributes;

namespace Benchmarking
{
    [MemoryDiagnoser]
    public class ReadOnlySpanBenchmark
    {
        private const string DateString = "02 01 2022";

        [Benchmark]
        public (int day, int month, int year) GetDateFromString()
        {
            var date = DateString;
            var dayString = date.Substring(0, 2);
            var monthString = date.Substring(3, 2);
            var yearString = date.Substring(6, 2);
            var day = int.Parse(dayString);
            var month = int.Parse(monthString);
            var year = int.Parse(yearString);
            return (day, month, year);
        }

        [Benchmark]
        public (int day, int month, int year) GetDateFromStringReadOnlySpan()
        {
            ReadOnlySpan<char> date = DateString;
            var dayString = date.Slice(0, 2);
            var monthString = date.Slice(3, 2);
            var yearString = date.Slice(6, 2);
            var day = int.Parse(dayString);
            var month = int.Parse(monthString);
            var year = int.Parse(yearString);
            return (day, month, year);
        }
    }
}
