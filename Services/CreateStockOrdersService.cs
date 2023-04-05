using Microsoft.Extensions.Logging;
using Entities;

using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CreateStockOrdersService: ICreateStockOrdersService
    {
        private readonly StockMarketDbContext _dbContext;
        private readonly ILogger<CreateStockOrdersService> _logger;

        public CreateStockOrdersService(StockMarketDbContext dbContext,
                                        ILogger<CreateStockOrdersService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public OrderResponse CreateBuyOrder(OrderRequest orderRequest)
        {
            _logger.LogInformation("CreateBuyOrder of StocksService");

            Order order = orderRequest.ToOrder();
            order.OrderID = Guid.NewGuid();

            _dbContext.sp_InsertBuyOrder(order);
            _dbContext.SaveChangesAsync();

            return order.ToOrderResponse();
        }

        public OrderResponse CreateSellOrder(OrderRequest orderRequest)
        {
            _logger.LogInformation("CreateSellOrder of StocksService");

            Order order = orderRequest.ToOrder();
            order.OrderID = Guid.NewGuid();

            _dbContext.sp_InsertSellOrder(order);
            _dbContext.SaveChangesAsync();

            return order.ToOrderResponse();
        }
    }
}
