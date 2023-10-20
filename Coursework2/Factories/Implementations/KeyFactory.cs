using System.Reflection.Metadata.Ecma335;
using Coursework2.Factories.Interfaces;

namespace Coursework2.Factories.Implementations
{
    public class KeyFactory : IKeyFactory
    {
        private readonly string[] _apiKeys =
        {

            "58627b97-334b-4e06-b054-91c53add8926",
            "fb4e2868-7612-4a68-a305-6cb8bc25fa8e"
        };
        public readonly HttpClient _client;
        private int _currentIndex;
        public KeyFactory()
        {
            _currentIndex = 0;
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://pro-api.coinmarketcap.com");
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("Accept-Encoding", "deflate, gzip");
        }

        public string[] ApiKeys { get => _apiKeys; }

        public async Task<string> GetNextValidAPIKeyAsync()
        {
            if (_apiKeys.Length == 0)
            {
                throw new InvalidOperationException("No API keys available.");
            }

            for (int i = 0; i < _apiKeys.Length; i++)
            {
                string nextKey = _apiKeys[_currentIndex];
                bool isValid = await IsApiKeyValid(nextKey);

                if (isValid)
                {
                    // Якщо ключ валідний, повертаємо його
                    _currentIndex = (_currentIndex + 1) % _apiKeys.Length; // Переключити на наступний ключ
                    return nextKey;
                }

                _currentIndex = (_currentIndex + 1) % _apiKeys.Length; // Переходимо до наступного ключа
            }

            throw new InvalidOperationException("All API keys are invalid.");
        }

        public async Task<bool> IsApiKeyValid(string apiKey)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", apiKey);

                    var response = await httpClient.GetAsync("https://pro-api.coinmarketcap.com/v1/cryptocurrency/map");

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                return false; 
            }
        }

        public string BuildUrl(string endpoint, Dictionary<string, object> queryParams)
        {
            string fullUrl = _client.BaseAddress + endpoint;
            if (queryParams != null && queryParams.Count != 0)
            {
                var query = System.Web.HttpUtility.ParseQueryString(string.Empty);
                foreach (var param in queryParams)
                {
                    if (param.Value is string)
                    {
                        query[param.Key] = (string)param.Value;
                    }
                    else if (param.Value is int)
                    {
                        query[param.Key] = param.Value.ToString();
                    }
                    else if (param.Value is bool)
                    {
                        query[param.Key] = (bool)param.Value ? "true" : "false";
                    }
                }
                fullUrl = _client.BaseAddress + endpoint + "?" + query;
            }

            return fullUrl;
        }
    }
}
