using Coursework2.Models;

namespace Coursework2.Interfaces
{
    public interface ICoinMarketCapFunctional
    {
        Task<CryptoCurrency> GetCoinMarketCapIdMapAsync();

        Task<FiatMapResponse> GetCoinMarketCapFiatAsync();

        Task<CryptoCurrencyMetaData> GetCoinMarketCapMetaDataAsync();

        Task<CryptoCurrenciesListMetaData> GetCryptoCurrenciesListMetaAsync(int fiatId = 1);

        Task<PriceConversionData> GetPriceConversionDataAsync();

        Task<ExchangeMetaData> GetExchangeMetaDataAsync();
    }
}
