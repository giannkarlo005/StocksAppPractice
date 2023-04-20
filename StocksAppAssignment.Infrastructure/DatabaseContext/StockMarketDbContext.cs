using Microsoft.EntityFrameworkCore;

using StocksAppAssignment.Core.DTO;
using StocksAppAssignment.Core.Entities;

namespace StocksAppAssignment.Infrastructure.DatabaseContext
{
    public class StockMarketDbContext: DbContext
    {
        public virtual DbSet<BuyOrder> BuyOrders { get; set; }
        public virtual DbSet<SellOrder> SellOrders { get; set; }

        public StockMarketDbContext(DbContextOptions options):
            base(options)
        {
        }

        public StockMarketDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BuyOrder>().ToTable("BuyOrders");
            modelBuilder.Entity<SellOrder>().ToTable("SellOrders");
        }
    }
}