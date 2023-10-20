﻿using Coursework2.Interfaces;
using Coursework2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
        public async Task<IActionResult> Index1()
        {
            try
            {
                var result = await _marketCapFunctional.GetCryptoCurrenciesListMetaAsync();
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
        public async Task<IActionResult> GetCurrency()
        {
            var result = await _marketCapFunctional.GetCoinMarketCapMetaDataAsync();
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetSelectedData(string ID)
        {
            var result = await _parseFunctional.ParseSelectedCurrency(ID);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetFiats()
        {
            var result = await _marketCapFunctional.GetCoinMarketCapFiatAsync();

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetSelectedFiats(int fiadID)
        {
            var result = await _marketCapFunctional.GetCryptoCurrenciesListMetaAsync(fiadID);
            return View("Index1", result);

        }
    }
}
