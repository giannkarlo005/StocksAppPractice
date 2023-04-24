using Microsoft.AspNetCore.Mvc.Filters;
using StocksAppAssignment.UI.Controllers.v1;

namespace StocksAppAssignment.UI.Filters.ActionFilters
{
    public class PdfDownloadActionFilter: IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();
            context.HttpContext.Response.Headers.Add("content-disposition", "attachment; filename=" + "Report.pdf");
        }
    }
}
