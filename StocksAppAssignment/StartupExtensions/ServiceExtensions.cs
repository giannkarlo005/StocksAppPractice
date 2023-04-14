using Microsoft.EntityFrameworkCore;

using StocksAppAssignment.Core.RepositoryContracts;
using StocksAppAssignment.Core.Services;
using StocksAppAssignment.Core.ServiceContracts;
using StocksAppAssignment.Infrastructure.DatabaseContext;
using StocksAppAssignment.Infrastructure.Repository;

namespace StocksAppAssignment.UI.StartupExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureServicesExtension(this IServiceCollection services,
                                                           IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.Configure<FinnhubApiOptions>(configuration.GetSection("finnhubapi"));

            services.AddScoped<IFinnhubService, FinnhubService>();
            services.AddScoped<ICreateStockOrdersService, CreateStockOrdersService>();
            services.AddScoped<IGetStockOrdersService, GetStockOrdersService>();
            services.AddScoped<IStockMarketRepository, StockMarketRepository>();

            services.AddDbContext<StockMarketDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
