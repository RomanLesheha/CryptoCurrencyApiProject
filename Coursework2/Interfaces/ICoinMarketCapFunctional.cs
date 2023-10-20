using Coursework2.Models;

namespace Coursework2.Interfaces
{
    public interface ICoinMarketCapFunctional
    {
        Task<CryptoCurrency> GetCoinMarketCapIdMapAsync();

        Task<FiatMapResponse> GetCoinMarketCapFiatAsync();

        Task<CryptoCurrencyMetaData> GetCoinMarketCapMetaDataAsync(int CurrencyId);

        Task<CryptoCurrenciesListMetaData> GetCryptoCurrenciesListMetaAsync(string FiatName = "USD");

        Task<PriceConversionData> GetPriceConversionDataAsync();

        Task<ExchangeMetaData> GetExchangeMetaDataAsync();
    }
}
