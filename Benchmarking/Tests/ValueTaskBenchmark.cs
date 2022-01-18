using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Benchmarking.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Benchmarking.Tests
{
    [MemoryDiagnoser]
    public class ValueTaskBenchmark
    {
        private readonly HttpClient _httpClient;
        private const string Ticker = "AAPL";
        private readonly IMemoryCache _memoryCache;
        private readonly JsonSerializerOptions _options;
        private const string _apiKey = "api key";


        public ValueTaskBenchmark()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _httpClient = new HttpClient();
            _options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        }

        [Benchmark]
        public async Task<FmpProfile> GetProfileTask()
        {
            if (!_memoryCache.TryGetValue(Ticker, out FmpProfile cacheValue))
            {
                var profileToAdd = await GetProfile(Ticker);
                _memoryCache.Set(Ticker, profileToAdd);
            }

            return cacheValue;
        }

        [Benchmark]
        public async ValueTask<FmpProfile> GetProfileValueTask()
        {
            if (!_memoryCache.TryGetValue(Ticker, out FmpProfile cacheValue))
            {
                var profileToAdd = await GetProfile(Ticker);
                _memoryCache.Set(Ticker, profileToAdd);
            }

            return cacheValue;
        }

        private async Task<FmpProfile> GetProfile(string ticker)
        {
            var response = await _httpClient.GetAsync(
                $"https://financialmodelingprep.com/api/v3/profile/{ticker}?apikey={_apiKey}");
            var profiles = await response.Content.ReadFromJsonAsync<List<FmpProfile>>(_options);
            return profiles.SingleOrDefault();
        }

    }
}
