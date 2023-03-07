using Entities;
using Microsoft.AspNetCore.Mvc;

namespace StocksAppAssignment.Controllers.ViewComponents
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
