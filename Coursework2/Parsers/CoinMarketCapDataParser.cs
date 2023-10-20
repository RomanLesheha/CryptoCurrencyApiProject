using Coursework2.Interfaces;
using Newtonsoft.Json;

namespace Coursework2.Parsers
{
    public class CoinMarketCapDataParser : IApiParser
    {
        public readonly HttpClient _client;
        private readonly string _apiKey = "fb4e2868-7612-4a68-a305-6cb8bc25fa8e";
        public CoinMarketCapDataParser()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.GZip
            };

            _client = new HttpClient(handler);
            _client.BaseAddress = new Uri("https://pro-api.coinmarketcap.com");
            _client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", _apiKey);
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<T> ParseAsync<T>(string endpoint)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
                catch (Exception ex)
            {
                return default;
            }
        }
    }
}
