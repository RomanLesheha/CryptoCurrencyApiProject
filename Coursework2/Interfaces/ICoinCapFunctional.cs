using Coursework2.Models;

namespace Coursework2.Interfaces
{
    public interface ICoinCapFunctional
    {
        Task<List<object>> ParseTopCurrency(int currentIndex);
        Task<AssetsResponse> ParseCurrency();
        Task<List<object>> ParseCurrencyHistory(string CurrencyName, string Interval);
    }
}
