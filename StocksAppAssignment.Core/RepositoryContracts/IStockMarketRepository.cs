using StocksAppAssignment.Core.DTO;

namespace StocksAppAssignment.Core.RepositoryContracts
{
    public interface IStockMarketRepository
    {
        /// <summary>
        /// Adds a new buy stock order to the database
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        Task<Order> CreateBuyOrder(Order order);
        /// <summary>
        /// Adds a new sell stock order to the database
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        Task<Order> CreateSellOrder(Order order);
        /// <summary>
        /// Fetches all buy orders from the database
        /// </summary>
        /// <returns></returns>
        Task<List<Order>> GetAllBuyOrders();
        /// <summary>
        /// Fetches all sell orders from the database
        /// </summary>
        /// <returns></returns>
        Task<List<Order>> GetAllSellOrders();
    }
}
