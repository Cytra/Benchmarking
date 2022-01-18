using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Benchmarking.Models;

namespace Benchmarking.FMP
{
    public class FmpClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        private const string _apiKey = "api key";


        public FmpClient()
        {
            _httpClient = new HttpClient();
            _options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        }

        public async Task<FmpProfile> GetProfileWithResult(string ticker)
        {
            var response = await _httpClient.GetAsync(
                $"https://financialmodelingprep.com/api/v3/profile/{ticker}?apikey={_apiKey}");
            var profiles = await response.Content.ReadFromJsonAsync<List<FmpProfile>>(_options);
            return profiles.SingleOrDefault();
        }

        public async Task<FmpProfile> GetProfileNewObject(string ticker)
        {
            var response = await _httpClient.GetAsync(
                $"https://financialmodelingprep.com/api/v3/profile/{ticker}?apikey={_apiKey}");
            return new FmpProfile();
        }
    }
}
