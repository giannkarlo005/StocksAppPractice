using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using StocksAppAssignment.Core.Entities;
using StocksAppAssignment.Core.Identities;

namespace StocksAppAssignment.Infrastructure.DatabaseContext
{
    public class StockMarketDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
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