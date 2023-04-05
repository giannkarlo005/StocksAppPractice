using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class StockMarketDbContext: DbContext
    {
        public virtual DbSet<Order> BuyOrders { get; set; }
        public virtual DbSet<Order> SellOrders { get; set; }

        public StockMarketDbContext(DbContextOptions options):
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("BuyOrders");
            modelBuilder.Entity<Order>().ToTable("SellOrders");
        }

        public int sp_InsertBuyOrder(Order buyOrder)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("OrderID", buyOrder.OrderID),
                    new SqlParameter("StockName", buyOrder.StockName),
                    new SqlParameter("StockSymbol", buyOrder.StockSymbol),
                    new SqlParameter("OrderQuantity", buyOrder.OrderQuantity),
                    new SqlParameter("OrderPrice", buyOrder.OrderPrice),
                    new SqlParameter("DateAndTimeOfOrder", buyOrder.DateAndTimeOfOrder),
                };

                return Database.ExecuteSqlRaw("EXECUTE [dbo].[InsertBuyOrder] " +
                                                  "@OrderID, " +
                                                  "@StockName, " +
                                                  "@StockSymbol, " +
                                                  "@OrderQuantity, " +
                                                  "@OrderPrice, " +
                                                  "@DateAndTimeOfOrder "
                                              , sqlParameters);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int sp_InsertSellOrder(Order sellOrder)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("OrderID", sellOrder.OrderID),
                    new SqlParameter("StockName", sellOrder.StockName),
                    new SqlParameter("StockSymbol", sellOrder.StockSymbol),
                    new SqlParameter("OrderQuantity", sellOrder.OrderQuantity),
                    new SqlParameter("OrderPrice", sellOrder.OrderPrice),
                    new SqlParameter("DateAndTimeOfOrder", sellOrder.DateAndTimeOfOrder),
                };

                return Database.ExecuteSqlRaw("EXECUTE [dbo].[InsertSellOrder] " +
                                                  "@OrderID, " +
                                                  "@StockName, " +
                                                  "@StockSymbol, " +
                                                  "@OrderQuantity, " +
                                                  "@OrderPrice, " +
                                                  "@DateAndTimeOfOrder "
                                              , sqlParameters);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Order> sp_GetBuyOrders()
        {
            try
            {
                return BuyOrders.FromSqlRaw("EXECUTE [dbo].[GetBuyOrders]").ToList();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public List<Order> sp_GetSellOrders()
        {
            try
            {
                return SellOrders.FromSqlRaw("EXECUTE [dbo].[GetSellOrders]").ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}