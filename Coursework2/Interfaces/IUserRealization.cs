using Microsoft.AspNetCore.Identity;
using Coursework2.Models.CoinMarketCapApiModels;

namespace Coursework2.Interfaces
{
    public interface IUserRealization
    {
        Task<bool> IsLikedCoinAsync(int CoinID);

        Task<bool> ManageFavourite(int? id, bool addToFavourite);

        string GetUserId();
    }
}
