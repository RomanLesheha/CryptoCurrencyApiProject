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

        public async Task<List<object>> ParseCurrencyOnMarkets(string CurrencyName, int Count)
        {
            var data = await _apiParser.GetApiParser(ApiServiceFactory.ApiParserType.CoinCap).ParseAsync<Markets>($"/v2/assets/{CurrencyName}/markets");

            var sortedData = data.data.Take(Count);

            List<object> _data = new List<object>();

            List<decimal> values = sortedData.Select(item => item.priceUsd).ToList();

            List<string> labels = sortedData.Select(item => item.exchangeId).ToList();

            _data.Add(labels);
            _data.Add(values);
            return _data;



        }
    }
}
