using Coursework2.DataBase;
using Coursework2.Factories.Implementations;
using Coursework2.Factories.Interfaces;
using Coursework2.Interfaces;
using Coursework2.Models;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Coursework2.Realizations
{
    public class CoinMarketCapService : ICoinMarketCapFunctional
    {
        private readonly DBContext _context;
        private readonly IApiParserFactory _apiParser;
        private readonly IKeyFactory _keyFactory;
        public CoinMarketCapService(IApiParserFactory apiParser,IKeyFactory keyFactory, DBContext context)
        {
            _context = context;
            _apiParser = apiParser;
            _keyFactory = keyFactory;
           
        }
        public async Task<CryptoCurrency> GetCoinMarketCapIdMapAsync()
        {
            string apiKey = await _keyFactory.GetNextValidAPIKeyAsync();

            try
            {
                string endpoint = "v1/cryptocurrency/map";
                var queryParams = new Dictionary<string, object>
                {
                    { "listing_status", "active" },
                    { "sort" ,"id" },
                    { "limit", 100 },
                };

                var response = await _apiParser.GetApiParser(ApiServiceFactory.ApiParserType.CoinMarketCap).ParseAsync<CryptoCurrency>(_keyFactory.BuildUrl(endpoint, queryParams));
                return response;
            }
            catch (Exception ex)
            {
                return new CryptoCurrency();
            }
           
            return null;
        }

        public async Task<CryptoCurrencyMetaData> GetCoinMarketCapMetaDataAsync(int CurrencyID)
        {
            try
            {
                string endpoint = string.Format($"v2/cryptocurrency/info?id={CurrencyID}");
                var queryParams = new Dictionary<string, object>
                {
                    
                };

                var response = await _apiParser.GetApiParser(ApiServiceFactory.ApiParserType.CoinMarketCap).ParseAsync<CryptoCurrencyMetaData>(_keyFactory.BuildUrl(endpoint, queryParams));

                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<FiatMapResponse> GetCoinMarketCapFiatAsync()
        {
            try
            {
                string endpoint = "/v1/fiat/map";
                var queryParams = new Dictionary<string, object>
                {
                    { "include_metals", true },
                    { "sort", "name" }
                };

                var response = await _apiParser.GetApiParser(ApiServiceFactory.ApiParserType.CoinMarketCap).ParseAsync<FiatMapResponse>(endpoint);

                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<CryptoCurrenciesListMetaData> GetCryptoCurrenciesListMetaAsync(Dictionary<string, object> queryParams)
        {
            string apiKey = await _keyFactory.GetNextValidAPIKeyAsync();
            try
            {
                string endpoint = $"v1/cryptocurrency/listings/latest";
                //var queryParams = new Dictionary<string, object>
                //{
                //    { "sort_dir", "desc" },
                //    { "sort", "price" },
                //};

                var response = await _apiParser.GetApiParser(ApiServiceFactory.ApiParserType.CoinMarketCap).ParseAsync<CryptoCurrenciesListMetaData>(_keyFactory.BuildUrl(endpoint, queryParams));

                var ids = response.data.Select(x => x.id).ToList();
                var metadata = await GetCoinMarketCapMetaDataAsync(ids);

                foreach (var dataItem in response.data)
                {
                    if (metadata.TryGetValue(dataItem.id, out var coinMetaData))
                    {
                        dataItem.urlLogo = coinMetaData.LogoUrl;
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong at the data parsing stage");
            }
        }

        private async Task<Dictionary<int, CryproCurrencyInDB>> GetCoinMarketCapMetaDataAsync(List<int> ids)
        {
            try
            {
                string endpoint = string.Format($"v2/cryptocurrency/info?id={string.Join(",", ids)}");
                var queryParams = new Dictionary<string, object>
                {
                };

                var response = await _apiParser.GetApiParser(ApiServiceFactory.ApiParserType.CoinMarketCap).ParseAsync<CryptoCurrencyMetaData>(_keyFactory.BuildUrl(endpoint, queryParams));

                var metaData = response.Data.ToDictionary(coin => coin.Value.Id, coin => new CryproCurrencyInDB
                {
                    LogoUrl = coin.Value.Logo
                });

                return metaData;
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong at the data parsing stage");
            }
        }

        public async Task<PriceConversionData> GetPriceConversionDataAsync()
        {
            try
            {
                string endpoint = "v2/tools/price-conversion";
                var queryParams = new Dictionary<string, object>
                {
                    { "amount", 100 },
                    { "id" ,1}, 
                    { "convert","USD"},

                };

                var response = await _apiParser.GetApiParser(ApiServiceFactory.ApiParserType.CoinMarketCap).ParseAsync<PriceConversionData>(_keyFactory.BuildUrl(endpoint, queryParams));

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong at the data parsing stage");
            }
        }
        private async Task<ExchangesListData> GetExchangesListData()
        {
            try
            {
                string endpoint = "v1/exchange/map";
                var queryParams = new Dictionary<string, object>
                {
                    { "listing_status", "active" },
                    { "sort" ,"id" },
                    { "limit",100},
                };
            
                var response = await _apiParser.GetApiParser(ApiServiceFactory.ApiParserType.CoinMarketCap).ParseAsync<ExchangesListData>(_keyFactory.BuildUrl(endpoint, queryParams));
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong at the data parsing stage");
            }
             
        }

        public async Task<ExchangeMetaData> GetExchangeMetaDataAsync()
        {
            string apiKey = await _keyFactory.GetNextValidAPIKeyAsync();

            try
            {
                var listExchanges = await GetExchangesListData();
                var ids = listExchanges.Data.Select(p=>p.id).ToList();
                string endpoint = $"v1/exchange/info?id={string.Join(",",ids)}";
                var queryParams = new Dictionary<string, object>
                {
                    
                };

                var response = await _apiParser.GetApiParser(ApiServiceFactory.ApiParserType.CoinMarketCap).ParseAsync<ExchangeMetaData>(_keyFactory.BuildUrl(endpoint, queryParams));
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong at the data parsing stage");
            }
        }

        //public async Task<List<CryproCurrencyInDB>> AddToDbInfoCoinMarketCapMetaData()
        //{
        //    CryptoCurrencyMetaData metaData = await GetCoinMarketCapMetaDataAsync();

        //    List<CryproCurrencyInDB> currenciesToSave = metaData.Data.Select(dataItem => new CryproCurrencyInDB
        //    {
        //        CurrencyId = dataItem.Value.Id,
        //        LogoUrl = dataItem.Value.Logo
        //    }).ToList();

        //    using (_context)
        //    {
        //        foreach (var currency in currenciesToSave)
        //        {
        //            var existingCurrency = _context.CryproCurrency.FirstOrDefault(c => c.Id == currency.CurrencyId);
        //            if (existingCurrency != null)
        //            {
        //                existingCurrency.LogoUrl = currency.LogoUrl;
        //            }
        //            else
        //            {
        //                _context.CryproCurrency.Add(currency);
        //            }
        //        }
        //        _context.SaveChanges();
        //    }
        //    return currenciesToSave;
        //}
    }

}
