using Microsoft.Extensions.Logging;

using StocksAppAssignment.Core.DTO;
using StocksAppAssignment.Core.RepositoryContracts;
using StocksAppAssignment.Core.ServiceContracts;

namespace StocksAppAssignment.Core.Services
{
    public class CreateStockOrdersService: ICreateStockOrdersService
    {
        private readonly IStockMarketRepository _stockMarketRepository;
        private readonly ILogger<CreateStockOrdersService> _logger;

        public CreateStockOrdersService(IStockMarketRepository stockMarketRepository,
                                        ILogger<CreateStockOrdersService> logger)
        {
            _stockMarketRepository = stockMarketRepository;
            _logger = logger;
        }

        public async Task<OrderResponse> CreateBuyOrder(OrderRequest? orderRequest)
        {
            _logger.LogInformation("CreateBuyOrder of StocksService");

            if(orderRequest == null)
            {
                throw new ArgumentNullException(nameof(orderRequest));
            }

            Order order = orderRequest.ToOrder();
            order.OrderID = Guid.NewGuid();

            await _stockMarketRepository.CreateBuyOrder(order);

            return order.ToOrderResponse();
        }

        public async Task<OrderResponse> CreateSellOrder(OrderRequest? orderRequest)
        {
            _logger.LogInformation("CreateSellOrder of StocksService");

            if (orderRequest == null)
            {
                throw new ArgumentNullException(nameof(orderRequest));
            }

            Order order = orderRequest.ToOrder();
            order.OrderID = Guid.NewGuid();

            await _stockMarketRepository.CreateBuyOrder(order);

            return order.ToOrderResponse();
        }
    }
}
