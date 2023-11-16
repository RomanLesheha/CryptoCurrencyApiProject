using Coursework2.Areas.Identity.Data;
using Coursework2.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Coursework2.Controllers
{
    public class UsersController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IUserRealization _user;

        public UsersController(IHttpContextAccessor http, UserManager<ApplicationUser> user, ApplicationDbContext context , IUserRealization user_)
        {
            _httpContextAccessor = http;
            _userManager = user;
            _context = context;
            _user = user_;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetRecentlyVisitedCurrency()
        {
            var user = GetUserId();
            List<RecentlyVisitedCurrency> recentlyVisitedCurrencies = _context.RecentlyVisitedCurrencies.Where(x => x.UserId == user).ToList();

            return Json(recentlyVisitedCurrencies);
        }
        [HttpPost]
        public async Task<bool> ManageFavourite(int? id, bool addToFavourite)
        {
            var result = await _user.ManageFavourite(id, addToFavourite);

            if (result)
            {
                return true; // Повернути успішну відповідь, якщо дія виконана успішно
            }
            else
            {
                return false; // Повернути помилку, якщо дія не вдалася
            }
        }

        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
