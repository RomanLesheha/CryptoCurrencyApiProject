using Coursework2.Models;

namespace Coursework2.Interfaces
{
    public interface ICoinCapFunctional
    {
        Task<AssetsResponse> ParseCurrency();
        Task<List<object>> ParseCurrencyHistory(string CurrencyName, string Interval);
        Task<List<object>> ParseCurrencyOnMarkets(string CurrencyName, int Count);
    }
}
