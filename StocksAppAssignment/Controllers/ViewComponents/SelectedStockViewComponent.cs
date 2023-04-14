using StocksAppAssignment.UI.Models;

using Microsoft.AspNetCore.Mvc;

using StocksAppAssignment.Core.DTO;
using StocksAppAssignment.Core.ServiceContracts;

namespace StocksAppAssignment.UI.Controllers.ViewComponents
{
    [ViewComponent]
    public class SelectedStockViewComponent: ViewComponent
    {
        private readonly IConfiguration _configuration;
        private readonly IFinnhubService _finnhubService;

        private readonly string FinnhubURL = "";
        private readonly string FinnhubToken = "";

        public SelectedStockViewComponent(IConfiguration configuration, IFinnhubService finnhubService)
        {
            _configuration = configuration;
            _finnhubService = finnhubService;

            IEnumerable<IConfigurationSection> finnhubApiOptions = _configuration.GetSection("finnhubapi").GetChildren();

            foreach (var section in finnhubApiOptions)
            {
                if (section.Key == "FinnhubURL")
                    FinnhubURL = section.Value ?? "";
                if (section.Key == "FinnhubToken")
                    FinnhubToken = section.Value ?? "" ;
            }


            _finnhubService.SetFinnhubUrlToken(FinnhubURL, FinnhubToken);
        }

        public async Task<IViewComponentResult> InvokeAsync(CompanyProfile? companyProfile)
        {
            return View("SelectedStock", companyProfile);
        }
    }
}
