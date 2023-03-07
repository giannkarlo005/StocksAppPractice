using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using Entities;
using Services;
using ServiceContracts;

namespace StocksAppAssignment.Controllers
{
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly FinnhubApiOptions _finnhubApiOptions;
        private readonly string FinnhubURL = "";
        private readonly string FinnhubToken = "";

        public TradeController(IOptions<FinnhubApiOptions> finnhubApiOptions, IFinnhubService finnhubService)
        {
            _finnhubApiOptions = finnhubApiOptions.Value;
            _finnhubService = finnhubService;

            FinnhubURL = _finnhubApiOptions.FinnhubURL;
            FinnhubToken = _finnhubApiOptions.FinnhubToken;

            _finnhubService.SetFinnhubUrlToken(FinnhubURL, FinnhubToken);
        }

        [Route("/")]
        [Route("/home")]
        public IActionResult GetAllStocks()
        {
            List<USExchange> usExchange = _finnhubService.GetAllStocks().Result;

            return View(usExchange);
        }

        [Route("/get-company-stockPrice/{stockSymbol}")]
        public IActionResult Index(string stockSymbol)
        {
            ViewBag.FinnhubToken = FinnhubToken;
            Dictionary<string, object>? companyProfileDict = _finnhubService.GetCompanyProfile(stockSymbol).Result;
            Dictionary<string, object>? companyStockPriceDict = _finnhubService.GetStockPriceQuote(stockSymbol).Result;

            string companyName = "";
            double companyStockPrice = 0;
            string companyLogo = "";

            if (companyProfileDict == null || companyStockPriceDict == null)
            {
                throw new ArgumentException("Stock Symbol not found");
            }

            foreach (string key in companyProfileDict.Keys)
            {
                switch(key)
                {
                    case "name":
                        companyName = Convert.ToString(companyProfileDict[key]);
                        break;
                    case "logo":
                        companyLogo = Convert.ToString(companyProfileDict[key]);
                        break;
                }
            }

            foreach (string key in companyStockPriceDict.Keys)
            {
                switch (key)
                {
                    case "c":
                        companyStockPrice = Convert.ToDouble(Convert.ToString(companyStockPriceDict[key]));
                        break;
                }
            }

            StockTrade companyProfile = new StockTrade()
            {
                StockSymbol = stockSymbol,
                StockName = companyName,
                StockLogo = companyLogo,
                Price = companyStockPrice
            };

            return View(companyProfile);
        }
    }
}
