using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Benchmarking.FMP;
using Benchmarking.Models;

namespace Benchmarking.Tests
{
    public  class ParallelApiBenchmark
    {
        private readonly FmpClient _fpmClient;
        private const int TaskCount = 3;
        private const string Ticker = "AAPL";
        public ParallelApiBenchmark()
        {
            _fpmClient = new FmpClient();
        }

        [Benchmark]
        public async Task<List<FmpProfile>> ForEachVersion()
        {
            var list = new List<FmpProfile>();

            var subscribedTask = Enumerable.Range(0, TaskCount)
                .Select(_ => new Func<Task<FmpProfile>>(() => _fpmClient.GetProfileNewObject(Ticker))).ToList();
            foreach (var task in subscribedTask)
            {
                list.Add(await task());
            }

            return list;
        }

        [Benchmark]
        public async Task<List<FmpProfile>> ThreeInlineCalls()
        {
            var list = new List<FmpProfile>();

            var result1 = await _fpmClient.GetProfileNewObject(Ticker);
            var result2 = await _fpmClient.GetProfileNewObject(Ticker);
            var result3 = await _fpmClient.GetProfileNewObject(Ticker);
            list.Add(result1);
            list.Add(result2);
            list.Add(result3);

            return list;
        }

        [Benchmark]
        public async Task<List<FmpProfile>> ParallelForVersion()
        {
            var list = new List<FmpProfile>();

            var subscribedTask = Enumerable.Range(0, TaskCount)
                .Select(_ => new Func<FmpProfile>(() => _fpmClient.GetProfileNewObject(Ticker).GetAwaiter().GetResult())).ToList();

            Parallel.For(0, subscribedTask.Count, i => list.Add(subscribedTask[i]()));

            return list;
        }

        //[Benchmark]
        public async Task<List<FmpProfile>> ParallelForEachVersionWith2MaxThreads()
        {
            var list = new List<FmpProfile>();
            var subscribedTask = Enumerable.Range(0, TaskCount)
                .Select(_ => new Func<FmpProfile>(() => _fpmClient.GetProfileNewObject(Ticker).GetAwaiter().GetResult())).ToList();

            Parallel.ForEach(subscribedTask, new ParallelOptions() { MaxDegreeOfParallelism = 2}, i => list.Add(i()));

            return list;
        }

        [Benchmark]
        public async Task<List<FmpProfile>> ParallelForEachVersionWith4MaxThreads()
        {
            var list = new List<FmpProfile>();
            var subscribedTask = Enumerable.Range(0, TaskCount)
                .Select(_ => new Func<FmpProfile>(() => _fpmClient.GetProfileNewObject(Ticker).GetAwaiter().GetResult())).ToList();

            Parallel.ForEach(subscribedTask, new ParallelOptions() { MaxDegreeOfParallelism = 4 }, i => list.Add(i()));

            return list;
        }

        [Benchmark]
        public async Task<List<FmpProfile>> WhenAll()
        {
            var subscribedTask = Enumerable.Range(0, TaskCount)
                .Select(_ => _fpmClient.GetProfileNewObject(Ticker));

            var result = await Task.WhenAll(subscribedTask);

            return result.ToList();
        }
    }
}
