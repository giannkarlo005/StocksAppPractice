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

        private OrderResponse ConvertToOrderResponse(Order order)
        {
            OrderResponse orderResponse = order.ToOrderResponse();

            return orderResponse;
        }

        public OrderResponse CreateBuyOrder(OrderRequest orderRequest)
        {
            _logger.LogInformation("CreateBuyOrder of StocksService");

            Order oder = orderRequest.ToOrder();
            oder.OrderID = Guid.NewGuid();

            _dbContext.sp_InsertBuyOrder(oder);
            _dbContext.SaveChangesAsync();

            return ConvertToOrderResponse(oder);
        }

        public OrderResponse CreateSellOrder(OrderRequest orderRequest)
        {
            _logger.LogInformation("CreateSellOrder of StocksService");

            Order oder = orderRequest.ToOrder();
            oder.OrderID = Guid.NewGuid();

            _dbContext.sp_InsertSellOrder(oder);
            _dbContext.SaveChangesAsync();

            return ConvertToOrderResponse(oder);
        }

        public List<OrderResponse> GetAllBuyOrders()
        {
            _logger.LogInformation("GetAllBuyOrders of StocksService");

            List<Order> buyOrders = _dbContext.sp_GetBuyOrders();
            return buyOrders.Select(buyOrder => buyOrder.ToOrderResponse()).ToList();
        }

        public List<OrderResponse> GetAllSellOrders()
        {
            _logger.LogInformation("GetAllSellOrders of StocksService");

            List<Order> sellOrders = _dbContext.sp_GetSellOrders();
            return sellOrders.Select(sellOrder => sellOrder.ToOrderResponse()).ToList();
        }
    }
}
