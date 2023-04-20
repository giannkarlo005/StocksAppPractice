using Microsoft.Extensions.Logging;

using StocksAppAssignment.Core.DTO;
using StocksAppAssignment.Core.Entities;
using StocksAppAssignment.Core.RepositoryContracts;
using StocksAppAssignment.Core.ServiceContracts;

namespace StocksAppAssignment.Core.Services
{
    public class GetStockOrdersService : IGetStockOrdersService
    {
        private readonly IStockMarketRepository _stockMarketRepository;
        private readonly ILogger<CreateStockOrdersService> _logger;

        public GetStockOrdersService(IStockMarketRepository stockMarketRepository,
                                     ILogger<CreateStockOrdersService> logger)
        {
            _stockMarketRepository = stockMarketRepository;
            _logger = logger;
        }

        public virtual async Task<List<OrderResponse>> GetAllBuyOrders()
        {
            _logger.LogInformation("GetAllBuyOrders of StocksService");

            List<BuyOrder> buyOrders = await _stockMarketRepository.GetAllBuyOrders();
            return buyOrders.Select(buyOrder => buyOrder.ToOrderResponse()).ToList();
        }

        public virtual async Task<List<OrderResponse>> GetAllSellOrders()
        {
            _logger.LogInformation("GetAllSellOrders of StocksService");

            List<SellOrder> sellOrders = await _stockMarketRepository.GetAllSellOrders();
            return sellOrders.Select(sellOrder => sellOrder.ToOrderResponse()).ToList();
        }

        public virtual async Task<List<OrderResponse>> GetFilteredBuyOrders(string stockSymbol)
        {
            _logger.LogInformation("GetAllBuyOrders of StocksService");

            List<BuyOrder> buyOrders = await _stockMarketRepository.GetAllBuyOrders();
            List<BuyOrder> filteredBuyOrders = buyOrders.Where(order => order.StockSymbol == stockSymbol).ToList();

            return filteredBuyOrders.Select(order => order.ToOrderResponse()).ToList();
        }

        public virtual async Task<List<OrderResponse>> GetFilteredSellOrders(string stockSymbol)
        {
            _logger.LogInformation("GetAllSellOrders of StocksService");

            List<SellOrder> sellOrders = await _stockMarketRepository.GetAllSellOrders();
            List<SellOrder> filteredSellOrders = sellOrders.Where(order => order.StockSymbol == stockSymbol).ToList();

            return filteredSellOrders.Select(order => order.ToOrderResponse()).ToList();
        }
    }
}
