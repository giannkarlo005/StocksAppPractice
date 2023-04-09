using Microsoft.Extensions.Logging;
using Entities;

using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class GetStockOrdersService: IGetStockOrdersService
    {
        private readonly StockMarketDbContext _dbContext;
        private readonly ILogger<CreateStockOrdersService> _logger;

        public GetStockOrdersService(StockMarketDbContext dbContext,
                                     ILogger<CreateStockOrdersService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public virtual List<OrderResponse> GetAllBuyOrders()
        {
            _logger.LogInformation("GetAllBuyOrders of StocksService");

            List<Order> buyOrders = _dbContext.sp_GetBuyOrders();
            return buyOrders.Select(buyOrder => buyOrder.ToOrderResponse()).ToList();
        }

        public virtual List<OrderResponse> GetAllSellOrders()
        {
            _logger.LogInformation("GetAllSellOrders of StocksService");

            List<Order> sellOrders = _dbContext.sp_GetSellOrders();
            return sellOrders.Select(sellOrder => sellOrder.ToOrderResponse()).ToList();
        }
    }
}
