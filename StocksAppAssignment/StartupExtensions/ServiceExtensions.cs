using Microsoft.EntityFrameworkCore;

using Entities;
using ServiceContracts;
using Services;

namespace StocksAppAssignment.StartupExtensions
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

            services.AddDbContext<StockMarketDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
