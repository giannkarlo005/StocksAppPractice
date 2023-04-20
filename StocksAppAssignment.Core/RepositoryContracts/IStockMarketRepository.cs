using StocksAppAssignment.Core.DTO;
using StocksAppAssignment.Core.Entities;

namespace StocksAppAssignment.Core.RepositoryContracts
{
    public interface IStockMarketRepository
    {
        /// <summary>
        /// Adds a new buy stock order to the database
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        Task<Order> CreateBuyOrder(BuyOrder order);
        /// <summary>
        /// Adds a new sell stock order to the database
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        Task<Order> CreateSellOrder(SellOrder order);
        /// <summary>
        /// Fetches all buy orders from the database
        /// </summary>
        /// <returns></returns>
        Task<List<BuyOrder>> GetAllBuyOrders();
        /// <summary>
        /// Fetches all sell orders from the database
        /// </summary>
        /// <returns></returns>
        Task<List<SellOrder>> GetAllSellOrders();
    }
}
