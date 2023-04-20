using Microsoft.EntityFrameworkCore;

using StocksAppAssignment.Core.DTO;
using StocksAppAssignment.Core.Entities;
using StocksAppAssignment.Core.RepositoryContracts;
using StocksAppAssignment.Infrastructure.DatabaseContext;

namespace StocksAppAssignment.Infrastructure.Repository
{
    public class StockMarketRepository : IStockMarketRepository
    {
        private readonly StockMarketDbContext _dbContext;

        public StockMarketRepository(StockMarketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<Order> CreateBuyOrder(BuyOrder order)
        {
            _dbContext.BuyOrders.Add(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public virtual async Task<Order> CreateSellOrder(SellOrder order)
        {
            _dbContext.SellOrders.Add(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public virtual async Task<List<BuyOrder>> GetAllBuyOrders()
        {
            return await _dbContext.BuyOrders.ToListAsync();
        }

        public virtual async Task<List<SellOrder>> GetAllSellOrders()
        {
            return await _dbContext.SellOrders.ToListAsync();
        }
    }
}