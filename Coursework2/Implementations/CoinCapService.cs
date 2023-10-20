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

        public async Task<Assets> ParseSelectedCurrency(string SelectedID)
        {
            var data = await _apiParser.GetApiParser(ApiServiceFactory.ApiParserType.CoinCap).ParseAsync<SingleAssetResponse>($"/v2/assets/{SelectedID}");
            return data.data;
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
