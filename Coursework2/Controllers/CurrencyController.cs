using Coursework2.Areas.Identity.Data;
using Coursework2.Interfaces;
using Coursework2.Models;
using Coursework2.Models.CoinMarketCapApiModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;

namespace Coursework2.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ICoinCapFunctional _parseFunctional;
        private readonly ICoinMarketCapFunctional _marketCapFunctional;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public CurrencyController(ICoinCapFunctional parseFunctional , ICoinMarketCapFunctional marketCapFunctional , ApplicationDbContext dbContext, IHttpContextAccessor http, UserManager<ApplicationUser> user)
        {
            _parseFunctional = parseFunctional;
            _marketCapFunctional = marketCapFunctional;
            _context = dbContext;
            _httpContextAccessor = http;
            _userManager = user;    
        }
        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }

        [HttpGet]
        public async Task<IActionResult> CryptoCurrencyDetails(int CurrencyID)
        {
            var data = new CombinedData();
            data.MetaData = await _marketCapFunctional.GetCoinMarketCapMetaDataAsync(CurrencyID);
            var result = await _marketCapFunctional.GetCryptoCurrenciesListMetaAsync(null);

            data.Data = result.data.FirstOrDefault(x => x.id == CurrencyID);
            //ADD to receintly visited
            string userId = GetUserId();
            if( userId!= null)
            {
                var recentlyVisitedCurrency = new RecentlyVisitedCurrency
                {
                    Currency = CurrencyID.ToString(),
                    UserId = userId,
                };
                _context.RecentlyVisitedCurrencies.Add(recentlyVisitedCurrency);
            }
            
            var currency = await _context.PopularCurrencies.FirstOrDefaultAsync(c => c.CurrencyID == CurrencyID);
            if (currency == null)
            {
                currency = new PopularCurrencies
                {
                    CurrencyID = CurrencyID,
                    NumberOfVisits = 1 // Ініціалізуйте з 1, оскільки це перше відвідування
                };

                _context.PopularCurrencies.Add(currency);
            }
            else
            {
                // Якщо валюта знайдена, збільште кількість відвідувань на 1
                currency.NumberOfVisits += 1;
            }
            _context.SaveChanges();

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> CryptoCategoryDetails(string CategoryID)
        {
            var result = await _marketCapFunctional.GetCryptoCurrencyCategoryMetaDataAsync(CategoryID);
            return View(result);
        }
        [Route("Cryptocategories")]
        public async Task<IActionResult> CryptoCategories()
        {
            try
            {
                var result = await _marketCapFunctional.GetCryptoCurrencyCategoryListAsync();
                var sortedlist = result.SortByAvgPriceChange();
                return View(sortedlist);
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
            [Route("Cryptocurrencies")]
            public async Task<IActionResult> CryptoCurrencies()
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
        public async Task<IActionResult> CryptoExchanges()
        {
            var result = await _marketCapFunctional.GetExchangeMetaDataAsync();
            var sortedData = result.Data.OrderByDescending(kv => kv.Value.weekly_visits)
                               .ToDictionary(kv => kv.Key, kv => kv.Value);

            result.Data = sortedData;
            return View(result);
        }



        [HttpPost]
        public async Task<List<object>> GetDataHistory(string CurrencyName , string Interval)
        {
            var result = await _parseFunctional.ParseCurrencyHistory(CurrencyName, Interval);
            return result;
        }

        [HttpPost]
        public async Task<List<object>> GetDataMarkets(string CurrencyName, int Count)
        {
            var result = await _parseFunctional.ParseCurrencyOnMarkets(CurrencyName, Count=10);
            return result;
        }



        [HttpPost]
        public async Task<IActionResult> GetFiats()
        {
            var result = await _marketCapFunctional.GetCoinMarketCapFiatAsync();

            return Json(result);
        }

        
        [HttpPost]
        public async Task<IActionResult> GetFilteredResult(string queryParams)
        {
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(queryParams);

            // Створюємо об'єкт Dictionary<string, object> з отриманих даних
            Dictionary<string, object> queryParam = new Dictionary<string, object>();
            foreach (var item in data)
            {
                queryParam[item.Key] = item.Value;
            }
            var result = await _marketCapFunctional.GetCryptoCurrenciesListMetaAsync(queryParams:queryParam);
            return Json(result);

        }


        [HttpPost]
        public async Task<IActionResult> GetBitcoinEcoSystem(string tags)
        {
            
            var results = await _marketCapFunctional.GetCurrenciesByTags(tags);

            return Json(results);

        }
    }
}


