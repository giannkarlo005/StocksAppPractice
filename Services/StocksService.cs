using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class StocksService : IStocksService
    {
        private List<BuyOrder> _buyOrder;
        private List<SellOrder> _sellOrder;

        public StocksService()
        {
            _buyOrder = new List<BuyOrder>();
            _sellOrder = new List<SellOrder>();
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

            _buyOrder.Add(buyOrder);

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

            _sellOrder.Add(sellOrder);

            return ConvertToSellOrderResponse(sellOrder);
        }

        public List<BuyOrderResponse> GetAllBuyOrders()
        {
            return _buyOrder.Select(buyOrder => ConvertToBuyOrderResponse(buyOrder)).ToList();
        }

        public List<SellOrderResponse> GetAllSellOrders()
        {
            return _sellOrder.Select(sellOrder => ConvertToSellOrderResponse(sellOrder)).ToList();
        }
    }
}
