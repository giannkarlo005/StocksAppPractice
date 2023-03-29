using Microsoft.Extensions.Logging;

using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

using Serilog;
using SerilogTimings;

namespace Services
{
    public class StocksService : IStocksService
    {
        private readonly StockMarketDbContext _dbContext;
        private readonly ILogger<StocksService> _logger;

        public StocksService(StockMarketDbContext dbContext, ILogger<StocksService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        private BuyOrderResponse ConvertToBuyOrderResponse(BuyOrder buyOrder)
        {
            BuyOrderResponse buyOrderResponse = buyOrder.ToBuyOrderResponse();

            return buyOrderResponse;
        }
        private SellOrderResponse ConvertToSellOrderResponse(SellOrder sellOrder)
        {
            SellOrderResponse sellOrderResponse = sellOrder.ToSellOrderResponse();

            return sellOrderResponse;
        }

        public BuyOrderResponse CreateBuyOrder(BuyOrderRequest buyOrderRequest)
        {
            _logger.LogInformation("CreateBuyOrder of StocksService");

            if(buyOrderRequest == null)
            {
                _logger.LogError("Buy Order Request is null");
                throw new ArgumentNullException(nameof(buyOrderRequest));
            }
            if(buyOrderRequest.OrderQuantity <= 0 || buyOrderRequest.OrderQuantity > 10000)
            {
                _logger.LogError("Buy Order Quantity is {OrderQuantity}", Convert.ToString(buyOrderRequest.OrderQuantity));
                throw new ArgumentException(nameof(buyOrderRequest.OrderQuantity));
            }
            if(buyOrderRequest.OrderPrice <= 0 || buyOrderRequest.OrderPrice > 10000)
            {
                _logger.LogError("Buy Order Price is {OrderPrice}", Convert.ToString(buyOrderRequest.OrderQuantity));
                throw new ArgumentException(nameof(buyOrderRequest.OrderPrice));
            }
            if(buyOrderRequest.StockSymbol == null)
            {
                _logger.LogError("Buy Order Stock Symbol is null");
                throw new ArgumentException(nameof(buyOrderRequest.StockSymbol));
            }
            if(DateTime.Compare(buyOrderRequest.DateAndTimeOfOrder.Value, DateTime.Parse("2000-01-01")) < 0)
            {
                _logger.LogError("Buy Order Date and Time of Order is {DateAndTimeOfOrder}", Convert.ToString(buyOrderRequest.DateAndTimeOfOrder));
                throw new ArgumentException(nameof(buyOrderRequest.DateAndTimeOfOrder));
            }

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            buyOrder.OrderID = Guid.NewGuid();

            _dbContext.sp_InsertBuyOrder(buyOrder);
            _dbContext.SaveChangesAsync();

            return ConvertToBuyOrderResponse(buyOrder);
        }

        public SellOrderResponse CreateSellOrder(SellOrderRequest sellOrderRequest)
        {
            _logger.LogInformation("CreateSellOrder of StocksService");

            if (sellOrderRequest == null)
            {
                _logger.LogError("Sell Order Request is null");
                throw new ArgumentNullException(nameof(sellOrderRequest));
            }
            if (sellOrderRequest.OrderQuantity <= 0 || sellOrderRequest.OrderQuantity > 10000)
            {
                _logger.LogError("Sell Order Quantity is {OrderQuantity}", Convert.ToString(sellOrderRequest.OrderQuantity));
                throw new ArgumentException(nameof(sellOrderRequest.OrderQuantity));
            }
            if (sellOrderRequest.OrderPrice <= 1 || sellOrderRequest.OrderPrice > 10000)
            {
                _logger.LogError("Sell Order Price is {OrderPrice}", Convert.ToString(sellOrderRequest.OrderQuantity));
                throw new ArgumentException(nameof(sellOrderRequest.OrderPrice));
            }
            if (sellOrderRequest.StockSymbol == null)
            {
                _logger.LogError("Sell Order Stock Symbol is null");
                throw new ArgumentException(nameof(sellOrderRequest.StockSymbol));
            }
            if (DateTime.Compare(sellOrderRequest.DateAndTimeOfOrder.Value, DateTime.Parse("2000-01-01")) < 0)
            {
                _logger.LogError("Sell Order Date and Time of Order is {DateAndTimeOfOrder}", Convert.ToString(sellOrderRequest.DateAndTimeOfOrder));
                throw new ArgumentException(nameof(sellOrderRequest.DateAndTimeOfOrder));
            }

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            sellOrder.OrderID = Guid.NewGuid();

            _dbContext.sp_InsertSellOrder(sellOrder);
            _dbContext.SaveChangesAsync();

            return ConvertToSellOrderResponse(sellOrder);
        }

        public List<BuyOrderResponse> GetAllBuyOrders()
        {
            _logger.LogInformation("GetAllBuyOrders of StocksService");

            List<BuyOrder> buyOrders = _dbContext.sp_GetBuyOrders();
            return buyOrders.Select(buyOrder => buyOrder.ToBuyOrderResponse()).ToList();
        }

        public List<SellOrderResponse> GetAllSellOrders()
        {
            _logger.LogInformation("GetAllSellOrders of StocksService");

            List<SellOrder> sellOrders = _dbContext.sp_GetSellOrders();
            return sellOrders.Select(sellOrder => sellOrder.ToSellOrderResponse()).ToList();
        }
    }
}
