using StocksAppAssignment;
using Services;
using ServiceContracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Configure<FinnhubApiOptions>(builder.Configuration.GetSection("finnhubapi"));
builder.Services.Add(new ServiceDescriptor(
    typeof(IFinnhubService),
    typeof(FinnhubService),
    ServiceLifetime.Singleton
));
builder.Services.Add(new ServiceDescriptor(
    typeof(IStocksService),
    typeof(StocksService),
    ServiceLifetime.Singleton
));
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
