using Microsoft.AspNetCore.Mvc;

using Serilog;

using StocksAppAssignment.Core.DTO;
using StocksAppAssignment.Core.ServiceContracts;
using StocksAppAssignment.UI.Models;

namespace StocksAppAssignment.UI.Controllers
{
    public class StocksController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IFinnhubService _finnhubService;

        private readonly ILogger<StocksController> _logger;
        private readonly IDiagnosticContext _diagnosticContext;

        private readonly string FinnhubURL = "";
        private readonly string FinnhubToken = "";

        public StocksController(IConfiguration configuration,
                                IFinnhubService finnhubService,
                                ILogger<StocksController> logger,
                                IDiagnosticContext diagnosticContext) 
        { 
            _configuration = configuration;
            _finnhubService = finnhubService;

            _logger = logger;
            _diagnosticContext = diagnosticContext;

            IEnumerable<IConfigurationSection> finnhubApiOptions = _configuration.GetSection("finnhubapi").GetChildren();

            foreach (var section in finnhubApiOptions)
            {
                if (section.Key == "FinnhubURL")
                    FinnhubURL = section.Value ?? "";
                if (section.Key == "FinnhubToken")
                    FinnhubToken = section.Value ?? "";
            }
            
            _finnhubService.SetFinnhubUrlToken(FinnhubURL, FinnhubToken);
        }

        private List<string> getTop25StocksList()
        {
            _logger.LogInformation("getTop25StocksList of StocksController");

            var tradingOptions = _configuration.GetSection("TradingOptions").GetChildren();
            string top25StocksStr = "";
            foreach (var section in tradingOptions)
            {
                if (section.Key == "Top25PopularStocks")
                {
                    top25StocksStr = section.Value ?? "";
                    break;
                }
            }

            return top25StocksStr.Split(",").ToList();
        }

        private List<Stock> getPopularStocksProfiles(List<string> top25StocksList)
        {
            _logger.LogInformation("getPopularStocksProfiles of StocksController");
            _diagnosticContext.Set("Top25StocksList", top25StocksList);

            List<Stock> popularStocks = new List<Stock>();
            List<USExchange> usExchange = _finnhubService.GetAllStocks().Result;
            foreach (var exchange in usExchange)
            {
                if (!top25StocksList.Contains(exchange.Symbol ?? ""))
                {
                    continue;
                }
                popularStocks.Add(new Stock()
                {
                    StockName = exchange.Description,
                    StockSymbol = exchange.Symbol
                });
            }
            popularStocks.OrderBy(stock => stock.StockSymbol);

            return popularStocks;
        }

        private CompanyProfile getCompanyProfile(string StockSymbol)
        {
            _logger.LogInformation("getCompanyProfile of StocksController");
            _logger.LogDebug($"StockSymbol: {StockSymbol}");

            string? companyName = "";
            string? companyLogo = "";
            string? country = "";
            string? currency = "";
            string? exchange = "";
            string? finnhubIndustry = "";
            string? marketCapitalization = "";
            string? webURL = "";

            Dictionary<string, object>? companyProfileDict = _finnhubService.GetCompanyProfile(StockSymbol).Result;
            _diagnosticContext.Set("CompanyProfileDictionary", companyProfileDict);

            if (companyProfileDict == null)
            {
                throw new ArgumentException("Stock Symbol not found");
            }

            foreach (string key in companyProfileDict.Keys)
            {
                switch (key)
                {
                    case "name":
                        companyName = Convert.ToString(companyProfileDict[key]);
                        break;
                    case "logo":
                        companyLogo = Convert.ToString(companyProfileDict[key]);
                        break;
                    case "country":
                        country = Convert.ToString(companyProfileDict[key]);
                        break;
                    case "currency":
                        currency = Convert.ToString(companyProfileDict[key]);
                        break;
                    case "exchange":
                        exchange = Convert.ToString(companyProfileDict[key]);
                        break;
                    case "finnhubIndustry":
                        finnhubIndustry = Convert.ToString(companyProfileDict[key]);
                        break;
                    case "marketCapitalization":
                        marketCapitalization = Convert.ToString(companyProfileDict[key]);
                        break;
                    case "webURL":
                        webURL = Convert.ToString(companyProfileDict[key]);
                        break;
                }
            }
            CompanyProfile companyProfile = new CompanyProfile()
            {
                StockSymbol = StockSymbol,
                CompanyName = companyName,
                CompanyLogo = companyLogo,
                Country = country,
                Currency = currency,
                Exchange = exchange,
                FinnHubIndustry = finnhubIndustry,
                MarketCapitalization = marketCapitalization,
                WebURL = webURL
            };

            return companyProfile;
        }

        [HttpGet]
        [Route("stocks/explore")]
        public IActionResult Explore()
        {
            _logger.LogInformation("Get Explore of StocksController");

            List<string> top25StocksList = getTop25StocksList();
            List<Stock> popularStocks = getPopularStocksProfiles(top25StocksList);

            _diagnosticContext.Set("Top25PopularStocks", popularStocks);

            ViewBag.StockSymbol = null;
            ViewBag.CompanyProfile = null;

            ViewBag.PopularStocks = popularStocks;

            return View(popularStocks);
        }

        [HttpPost]
        [Route("stocks/explore")]
        public IActionResult Explore([FromBody] string? StockSymbol)
        {
            if (String.IsNullOrEmpty(StockSymbol))
            {
                return BadRequest("Stock Symbol should not be empty");
            }

            _logger.LogInformation("Post Explore of StocksController");
            _logger.LogDebug($"StockSymbol: {StockSymbol}");

            ViewBag.StockSymbol = StockSymbol;

            double companyStockPrice = 0;
            Dictionary<string, object>? companyStockPriceDict = new Dictionary<string, object>();

            companyStockPriceDict = _finnhubService.GetStockPriceQuote(StockSymbol).Result;
            if (companyStockPriceDict != null)
            {
                foreach (string key in companyStockPriceDict.Keys)
                {
                    switch (key)
                    {
                        case "c":
                            companyStockPrice = Convert.ToDouble(Convert.ToString(companyStockPriceDict[key]));
                            break;
                    }
                }
            }

            CompanyProfile companyProfile = getCompanyProfile(StockSymbol);
            companyProfile.StockPrice = companyStockPrice;

            return ViewComponent("SelectedStock", companyProfile);
        }
    }
}
