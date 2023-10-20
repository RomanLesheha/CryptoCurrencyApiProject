using Coursework2.Interfaces;
using Newtonsoft.Json;

namespace Coursework2.Parsers
{
    public class CoinCapDataParser : IApiParser
    {
        private readonly HttpClient _client;
        public CoinCapDataParser()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://api.coincap.io");
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
