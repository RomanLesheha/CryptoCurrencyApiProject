using Coursework2.Areas.Identity.Data;
using Coursework2.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coursework2.Implementations
{
    public class UserService : IUserRealization
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public UserService(IHttpContextAccessor http, UserManager<ApplicationUser> user, ApplicationDbContext context)
        {
            _httpContextAccessor = http;
            _userManager = user;
            _context = context;
        }
        public async Task<bool> ManageFavourite(int? id, bool addToFavourite)
        {
            if (id == null || _context.FavouriteCurrencies == null)
            {
                return false;
            }

            var user = GetUserId();

            if (user == null)
            {
                return false;
            }

            if (addToFavourite)
            {
                _context.FavouriteCurrencies.Add(new FavouriteCurrency
                {
                    UserId = user,
                    Currency = id.ToString()
                });

                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                var coin = await _context.FavouriteCurrencies
                    .FirstOrDefaultAsync(m => m.Currency == id.ToString() && m.UserId == user);
                if (coin != null)
                {
                    _context.FavouriteCurrencies.Remove(coin);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> IsLikedCoinAsync(int CoinID)
        {
            var user = GetUserId();
            if (user == null) { return false; }
            var product = await _context.FavouriteCurrencies
               .FirstOrDefaultAsync(m => m.Currency == CoinID.ToString() && m.UserId == user);
            if (product == null)
            {
                return false;
            }
            return true;
        }

        public string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
