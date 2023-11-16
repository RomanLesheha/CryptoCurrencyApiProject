
using Coursework2.Areas.Identity.Data;
using Coursework2.Interfaces;
using Coursework2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Coursework2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ICoinMarketCapFunctional _marketCapFunctional;
        public HomeController(ILogger<HomeController> logger , ICoinMarketCapFunctional marketCapFunctional, IHttpContextAccessor http , UserManager<ApplicationUser> user,  ApplicationDbContext context)
        {
            _logger = logger;
            _httpContextAccessor = http;
            _userManager = user;
            _context = context;
            _marketCapFunctional = marketCapFunctional;
        }

        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var result = await _marketCapFunctional.GetCryptoCurrenciesListMetaAsync(null);
                return View(result);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    ErrorMessage = ex.Message,
                    ExceptionDetails = ex.ToString(), // Деталі Exception
                    StackTrace = ex.StackTrace
                };
                return View("Error", errorModel);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}