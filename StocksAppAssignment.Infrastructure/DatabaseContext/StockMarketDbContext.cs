using Microsoft.EntityFrameworkCore;

using StocksAppAssignment.Core.DTO;

namespace StocksAppAssignment.Infrastructure.DatabaseContext
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
    }
}