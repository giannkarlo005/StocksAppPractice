using StocksAppAssignment.Core.DTO;

namespace StocksAppAssignment.Core.ServiceContracts
{
    public interface IGetStockOrdersService
    {
        Task<List<OrderResponse>> GetAllBuyOrders();
        Task<List<OrderResponse>> GetAllSellOrders();
        Task<List<OrderResponse>> GetFilteredBuyOrders(string stockSymbol);
        Task<List<OrderResponse>> GetFilteredSellOrders(string stockSymbol);
    }
}
