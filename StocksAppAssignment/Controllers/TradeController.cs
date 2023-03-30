using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using StocksAppAssignment.Models;
using StocksAppAssignment.Filters.ActionFilters;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using Serilog;

namespace StocksAppAssignment.Controllers
{
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly IStocksService _stocksService;
        private readonly ILogger<TradeController> _logger;
        private readonly IDiagnosticContext _diagnosticContext;

        private readonly FinnhubApiOptions _finnhubApiOptions;

        private readonly string FinnhubURL = "";
        private readonly string FinnhubToken = "";

        public TradeController(IOptions<FinnhubApiOptions> finnhubApiOptions,
                               IFinnhubService finnhubService,
                               IStocksService stocksService,
                               ILogger<TradeController> logger,
                               IDiagnosticContext diagnosticContext)
        {
            _finnhubApiOptions = finnhubApiOptions.Value;
            _finnhubService = finnhubService;

            FinnhubURL = _finnhubApiOptions.FinnhubURL;
            FinnhubToken = _finnhubApiOptions.FinnhubToken;

            _finnhubService.SetFinnhubUrlToken(FinnhubURL, FinnhubToken);

            _stocksService = stocksService;
            _logger = logger;
            _diagnosticContext = diagnosticContext;
        }

        [Route("/")]
        [Route("/home")]
        public async Task<IActionResult> GetAllStocks()
        {
            _logger.LogInformation("GetAllStocks of TradeController");

            List<USExchange> usExchange = _finnhubService.GetAllStocks().Result;

            return View("GetAllStocks", usExchange);
        }

        [Route("/get-company-stockPrice/{stockSymbol}")]
        public async Task<IActionResult> Index(string? stockSymbol)
        {
            _logger.LogInformation("Index of TradeController");
            _logger.LogDebug($"StockSymbol: {stockSymbol}");

            if (String.IsNullOrEmpty(stockSymbol))
            {
                return null;
            }

            ViewBag.FinnhubToken = FinnhubToken;
            ViewBag.StockSymbol = stockSymbol;

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

            return View("Index", companyProfile);
        }

        [HttpPost]
        [Route("/buy-order")]
        //[TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<IActionResult> BuyOrder([FromBody] OrderRequest orderRequest)
        {
            _logger.LogInformation("BuyOrder of TradeController");
            _diagnosticContext.Set("BuyOrderRequest", orderRequest);

            if (!orderRequest.DateAndTimeOfOrder.HasValue)
            {
                orderRequest.DateAndTimeOfOrder = DateTime.Now;
            }
            _stocksService.CreateBuyOrder(orderRequest);

            return RedirectToAction("GetOrders", "Trade", orderRequest.StockSymbol);
        }

        [HttpPost]
        [Route("/sell-order")]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<IActionResult> SellOrder([FromBody] OrderRequest orderRequest)
        {
            _logger.LogInformation("SellOrder of TradeController");
            _diagnosticContext.Set("SellOrderRequest", orderRequest);

            if (!orderRequest.DateAndTimeOfOrder.HasValue)
            {
                orderRequest.DateAndTimeOfOrder = DateTime.Now;
            }
            _stocksService.CreateSellOrder(orderRequest);

            return RedirectToAction("GetOrders", "Trade", orderRequest.StockSymbol);
        }

        [Route("/get-orders/{stockSymbol?}")]
        public IActionResult GetOrders(string? stockSymbol)
        {
            _logger.LogInformation("GetOrders of TradeController");
            _logger.LogDebug($"StockSymbol: {stockSymbol}");

            if (String.IsNullOrEmpty(stockSymbol))
            {
                return null;
            }

            ViewBag.StockSymbol = stockSymbol;
            IEnumerable<OrderResponse> filteredBuyOrders = _stocksService.GetAllBuyOrders().Where(x => x.StockSymbol == stockSymbol);
            IEnumerable<OrderResponse> filteredSellOrders = _stocksService.GetAllSellOrders().Where(x => x.StockSymbol == stockSymbol);

            List<OrderResponse> buyOrders = filteredBuyOrders.ToList();
            List<OrderResponse> sellOrders = filteredSellOrders.ToList();

            Orders stockTrade = new Orders()
            {
                BuyOrders = buyOrders,
                SellOrders = sellOrders
            };

            return View("Orders", stockTrade);
        }

        [Route("/orders-pdf/{stockSymbol}")]
        public IActionResult OrdersPDF(string stockSymbol)
        {
            _logger.LogInformation("OrdersPDF of TradeController");
            _logger.LogDebug($"StockSymbol: {stockSymbol}");

            ViewBag.StockSymbol = stockSymbol;
            IEnumerable<OrderResponse> filteredBuyOrders = _stocksService.GetAllBuyOrders().Where(x => x.StockSymbol == stockSymbol); ;
            IEnumerable<OrderResponse> filteredSellOrders = _stocksService.GetAllSellOrders().Where(x => x.StockSymbol == stockSymbol); ;

            List<OrderResponse> buyOrderResponses = filteredBuyOrders.ToList();
            List<OrderResponse> sellOrderResponses = filteredSellOrders.ToList();

            List<OrderSummary> orderSummaryList = new List<OrderSummary>();


            foreach (OrderResponse buyOrder in buyOrderResponses)
            {
                double orderQuantity = buyOrder.OrderQuantity;
                double orderPrice = buyOrder.OrderPrice;
                string tradeAmount = (orderQuantity * orderPrice).ToString();

                OrderSummary orderSummary = new OrderSummary()
                {
                    DateAndTime = buyOrder.DateAndTimeOfOrder,
                    Stock = $"{buyOrder.StockName} ({buyOrder.StockSymbol})",
                    OrderType = "Buy Order",
                    Quantity = orderQuantity,
                    Price = orderPrice,
                    TradeAmount = $"${tradeAmount}"
                };

                orderSummaryList.Add(orderSummary);
            }

            foreach (OrderResponse sellOrder in sellOrderResponses)
            {
                double orderQuantity = sellOrder.OrderQuantity;
                double orderPrice = sellOrder.OrderPrice;
                string tradeAmount = (orderQuantity * orderPrice).ToString();

                OrderSummary orderSummary = new OrderSummary()
                {
                    DateAndTime = sellOrder.DateAndTimeOfOrder,
                    Stock = $"{sellOrder.StockName} ({sellOrder.StockSymbol})",
                    OrderType = "Sell Order",
                    Quantity = orderQuantity,
                    Price = orderPrice,
                    TradeAmount = $"${tradeAmount}"
                };

                orderSummaryList.Add(orderSummary);
            }

            orderSummaryList = orderSummaryList.OrderByDescending(x => x.DateAndTime).ToList();

            return new ViewAsPdf("OrdersPDF", orderSummaryList, ViewData)
            {
                PageMargins = new Margins()
                {
                    Top = 10,
                    Bottom = 10,
                    Left = 10,
                    Right = 10
                },
                PageOrientation = Orientation.Portrait
            };
        }
    }
}