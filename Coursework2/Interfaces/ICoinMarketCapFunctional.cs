using Coursework2.Models;

namespace Coursework2.Interfaces
{
    public interface ICoinMarketCapFunctional
    {
        Task<CryptoCurrency> GetCoinMarketCapIdMapAsync();

        Task<FiatMapResponse> GetCoinMarketCapFiatAsync();

        Task<CryptoCurrencyMetaData> GetCoinMarketCapMetaDataAsync(int CurrencyId);

        Task<CryptoCurrenciesListMetaData> GetCryptoCurrenciesListMetaAsync(Dictionary<string, object> queryParams);
        Task<CryptoCurrenciesListMetaData> GetCurrenciesByTags(string tags);

        Task<PriceConversionData> GetPriceConversionDataAsync();

        Task<ExchangeMetaData> GetExchangeMetaDataAsync();

        Task<CryptoCurrencyCategoryList> GetCryptoCurrencyCategoryListAsync();
        Task<CryptoCurrencyCategoryMetaData> GetCryptoCurrencyCategoryMetaDataAsync(string CategoryID);

        Task<CryptoGlobalMetricsData> GetGlobalMetricsAsync();

    }
}
