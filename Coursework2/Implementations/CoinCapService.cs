using Coursework2.Factories.Implementations;
using Coursework2.Factories.Interfaces;
using Coursework2.Interfaces;
using Coursework2.Models;
using System.Data;

namespace Coursework2.Realizations
{
    public class CoinCapService : ICoinCapFunctional
    {
        private readonly IApiParserFactory _apiParser;
        public CoinCapService(IApiParserFactory apiParser)
        {
            _apiParser = apiParser;
        }

        public async Task<AssetsResponse> ParseCurrency()
        {
            var data = await _apiParser.GetApiParser(ApiServiceFactory.ApiParserType.CoinCap).ParseAsync<AssetsResponse>($"/v2/assets/");
            return data;
        }

        public async Task<List<object>> ParseCurrencyHistory(string CurrencyName , string Interval)
        {
            var data = await _apiParser.GetApiParser(ApiServiceFactory.ApiParserType.CoinCap).ParseAsync<SelectedAsset>($"/v2/assets/{CurrencyName}/history?interval={Interval}");

            var priceUsd = data.data.Select(item => item.priceUsd).ToList();
            var time = data.data.Select(item => DateTimeOffset.FromUnixTimeMilliseconds(item.time).DateTime).ToList();
            List<object> _data = new List<object>();
            List<string> labels = time.Select(dt => dt.ToString()).ToList();

            List<decimal> Price = priceUsd;

            
            _data.Add(labels);
            _data.Add(Price);
            return _data;
        }

        public async Task<List<object>> ParseTopCurrency(int currentIndex)
        {
            var data = await _apiParser.GetApiParser(ApiServiceFactory.ApiParserType.CoinCap).ParseAsync<AssetsResponse>("/v2/assets");
            var sortedData = data.data.OrderByDescending(item => item.priceUsd);

            List<object> _data = new List<object>();
            int numberOfValues = 10;
            // Calculate the start and end indices based on the number of values and the current index
            int startIndex = currentIndex * numberOfValues;
            int endIndex = Math.Min(startIndex + numberOfValues, data.data.Count);

            List<string> labels = sortedData
                .Skip(startIndex)
                .Take(endIndex - startIndex)
                .Select(p => p.name)
                .ToList();

            List<decimal> Price = sortedData
                .Skip(startIndex)
                .Take(endIndex - startIndex)
                .Select(p => p.priceUsd)
                .ToList();

            List<string> Id = sortedData
                .Skip(startIndex)
                .Take(endIndex - startIndex)
                .Select(p => p.id)
                .ToList();

            _data.Add(labels);
            _data.Add(Price);
            _data.Add(Id);

            return _data;
        }
    }
}
