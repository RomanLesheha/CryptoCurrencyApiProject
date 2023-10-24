using Coursework2.Interfaces;
using Coursework2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;

namespace Coursework2.Controllers
{
    public class ChartsController : Controller
    {
        private readonly ICoinCapFunctional _parseFunctional;
        private readonly ICoinMarketCapFunctional _marketCapFunctional;

        public ChartsController(ICoinCapFunctional parseFunctional , ICoinMarketCapFunctional marketCapFunctional)
        {
            _parseFunctional = parseFunctional;
            _marketCapFunctional = marketCapFunctional;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int CurrencyID)
        {
            var result = await _marketCapFunctional.GetCoinMarketCapMetaDataAsync(CurrencyID);
            return View(result);
        }
       
        public async Task<IActionResult> Index1()
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
        public async Task<IActionResult> Index2()
        {
            var result = await _marketCapFunctional.GetExchangeMetaDataAsync();
            var sortedData = result.Data.OrderByDescending(kv => kv.Value.weekly_visits)
                               .ToDictionary(kv => kv.Key, kv => kv.Value);

            result.Data = sortedData;
            return View(result);
        }

        [HttpPost]
        public async Task<List<object>> GetData(int Index)
        {
            var result = await _parseFunctional.ParseTopCurrency(Index);
            return result;
        }

        [HttpPost]
        public async Task<List<object>> GetDataHistory(string CurrencyName , string Interval)
        {
            var result = await _parseFunctional.ParseCurrencyHistory(CurrencyName, Interval);
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
    }
}


