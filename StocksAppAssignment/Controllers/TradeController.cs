using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksAppAssignment.Models;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;

namespace StocksAppAssignment.Controllers
{
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly IStocksService _stocksService;
        private readonly FinnhubApiOptions _finnhubApiOptions;
        private readonly string FinnhubURL = "";
        private readonly string FinnhubToken = "";

        public TradeController(IOptions<FinnhubApiOptions> finnhubApiOptions, IFinnhubService finnhubService, IStocksService stocksService)
        {
            _finnhubApiOptions = finnhubApiOptions.Value;
            _finnhubService = finnhubService;

            FinnhubURL = _finnhubApiOptions.FinnhubURL;
            FinnhubToken = _finnhubApiOptions.FinnhubToken;

            _finnhubService.SetFinnhubUrlToken(FinnhubURL, FinnhubToken);

            _stocksService = stocksService;
        }

        [Route("/")]
        [Route("/home")]
        public IActionResult GetAllStocks()
        {
            List<USExchange> usExchange = _finnhubService.GetAllStocks().Result;

            return View(usExchange);
        }

        [Route("/get-company-stockPrice/{stockSymbol}")]
        public IActionResult Index(string? stockSymbol)
        {
            if(String.IsNullOrEmpty(stockSymbol))
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

            return View(companyProfile);
        }

        [HttpPost]
        [Route("/buy-order")]
        public void BuyOrder([FromBody] BuyOrderRequest buyOrderRequest)
        {
            if (!buyOrderRequest.DateAndTimeOfOrder.HasValue)
            {
                buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;
            }
            _stocksService.CreateBuyOrder(buyOrderRequest);
        }

        [HttpPost]
        [Route("/sell-order")]
        public void SellOrder([FromBody] SellOrderRequest sellOrderRequest)
        {
            if (!sellOrderRequest.DateAndTimeOfOrder.HasValue)
            {
                sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;
            }
            _stocksService.CreateSellOrder(sellOrderRequest);
        }

        [Route("/get-orders/{stockSymbol?}")]
        public IActionResult GetOrders(string? stockSymbol)
        {
            if (String.IsNullOrEmpty(stockSymbol))
            {
                return null;
            }

            ViewBag.StockSymbol = stockSymbol;
            IEnumerable<BuyOrderResponse> filteredBuyOrders = _stocksService.GetAllBuyOrders().Where(x => x.StockSymbol == stockSymbol);
            IEnumerable<SellOrderResponse> filteredSellOrders = _stocksService.GetAllSellOrders().Where(x => x.StockSymbol == stockSymbol);

            List<BuyOrderResponse> buyOrders = filteredBuyOrders.ToList();
            List<SellOrderResponse> sellOrders = filteredSellOrders.ToList();

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
            ViewBag.StockSymbol = stockSymbol;
            IEnumerable<BuyOrderResponse> filteredBuyOrders = _stocksService.GetAllBuyOrders().Where(x => x.StockSymbol == stockSymbol); ;
            IEnumerable<SellOrderResponse> filteredSellOrders = _stocksService.GetAllSellOrders().Where(x => x.StockSymbol == stockSymbol); ;

            List<BuyOrderResponse> buyOrderResponses = filteredBuyOrders.ToList();
            List<SellOrderResponse> sellOrderResponses = filteredSellOrders.ToList();

            List<OrderSummary> orderSummaryList = new List<OrderSummary>();


            foreach (BuyOrderResponse buyOrder in buyOrderResponses)
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

            foreach (SellOrderResponse sellOrder in sellOrderResponses)
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