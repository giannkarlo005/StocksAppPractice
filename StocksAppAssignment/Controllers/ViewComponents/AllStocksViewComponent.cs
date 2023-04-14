using Microsoft.AspNetCore.Mvc;

using StocksAppAssignment.Core.DTO;

namespace StocksAppAssignment.UI.Controllers.ViewComponents
{
    [ViewComponent]
    public class AllStocksViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<USExchange> usExchange)
        {
            return View("AllStockData", usExchange.OrderBy(x => x.Symbol).ToList());
        }
    }
}
