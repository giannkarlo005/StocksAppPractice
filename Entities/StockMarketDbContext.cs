using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class StockMarketDbContext: DbContext
    {
        public virtual DbSet<BuyOrder> BuyOrders { get; set; }
        public virtual DbSet<SellOrder> SellOrders { get; set; }

        public StockMarketDbContext(DbContextOptions options):
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuyOrder>().ToTable("BuyOrders");
            modelBuilder.Entity<SellOrder>().ToTable("SellOrders");
        }

        public int sp_InsertBuyOrder(BuyOrder buyOrder)
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

        public int sp_InsertSellOrder(SellOrder sellOrder)
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

        public List<BuyOrder> sp_GetBuyOrders()
        {
            return BuyOrders.FromSqlRaw("EXECUTE [dbo].[GetBuyOrders]").ToList();
        }

        public List<SellOrder> sp_GetSellOrders()
        {
            return SellOrders.FromSqlRaw("EXECUTE [dbo].[GetSellOrders]").ToList();
        }
    }
}