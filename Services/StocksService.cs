using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class StocksService : IStocksService
    {
        private readonly StockMarketDbContext _dbContext;

        public StocksService(StockMarketDbContext dbContext)
        {
            _dbContext = dbContext;
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
            if(buyOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(buyOrderRequest));
            }
            if(buyOrderRequest.OrderQuantity <= 0 || buyOrderRequest.OrderQuantity > 10000)
            {
                throw new ArgumentException(nameof(buyOrderRequest.OrderQuantity));
            }
            if(buyOrderRequest.OrderPrice <= 0 || buyOrderRequest.OrderPrice > 10000)
            {
                throw new ArgumentException(nameof(buyOrderRequest.OrderPrice));
            }
            if(buyOrderRequest.StockSymbol == null)
            {
                throw new ArgumentException(nameof(buyOrderRequest.StockSymbol));
            }
            if(DateTime.Compare(buyOrderRequest.DateAndTimeOfOrder.Value, DateTime.Parse("2000-01-01")) < 0)
            {
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
            if (sellOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(sellOrderRequest));
            }
            if (sellOrderRequest.OrderQuantity <= 0 || sellOrderRequest.OrderQuantity > 10000)
            {
                throw new ArgumentException(nameof(sellOrderRequest.OrderQuantity));
            }
            if (sellOrderRequest.OrderPrice <= 1 || sellOrderRequest.OrderPrice > 10000)
            {
                throw new ArgumentException(nameof(sellOrderRequest.OrderPrice));
            }
            if (sellOrderRequest.StockSymbol == null)
            {
                throw new ArgumentException(nameof(sellOrderRequest.StockSymbol));
            }
            if (DateTime.Compare(sellOrderRequest.DateAndTimeOfOrder.Value, DateTime.Parse("2000-01-01")) < 0)
            {
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
            List<BuyOrder> buyOrders = _dbContext.sp_GetBuyOrders();
            return buyOrders.Select(buyOrder => buyOrder.ToBuyOrderResponse()).ToList();
        }

        public List<SellOrderResponse> GetAllSellOrders()
        {
            List<SellOrder> sellOrders = _dbContext.sp_GetSellOrders();
            return sellOrders.Select(sellOrder => sellOrder.ToSellOrderResponse()).ToList();
        }
    }
}
