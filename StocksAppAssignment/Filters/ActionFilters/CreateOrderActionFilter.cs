using Microsoft.AspNetCore.Mvc.Filters;

using StocksAppAssignment.Core.DTO;
using StocksAppAssignment.UI.Controllers;

namespace StocksAppAssignment.UI.Filters.ActionFilters
{
    public class CreateOrderActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<CreateOrderActionFilter> _logger;

        public CreateOrderActionFilter(ILogger<CreateOrderActionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("{FilterName}.{Action} - Before", nameof(CreateOrderActionFilter), nameof(OnActionExecutionAsync));

            if (context.Controller is TradeController tradeController)
            {
                if(!tradeController.ModelState.IsValid)
                {
                    if (context.ActionArguments.ContainsKey("orderRequest"))
                    {
                        OrderRequest? orderRequest = context.ActionArguments["orderRequest"] as OrderRequest;

                        _logger.LogError("Order Quantity is {OrderQuantity}", Convert.ToString(orderRequest?.OrderQuantity));
                        _logger.LogError("Order Price is {OrderPrice}", Convert.ToString(orderRequest?.OrderQuantity));
                        _logger.LogError("Order Stock Symbol is null");
                        _logger.LogError("Order Date and Time of Order is {DateAndTimeOfOrder}", Convert.ToString(orderRequest?.DateAndTimeOfOrder));

                        tradeController.ViewBag.Errors = tradeController.ModelState.Values.SelectMany(x => x.Errors).Select(y => y.ErrorMessage).ToList();
                        context.Result = await tradeController.Index(orderRequest?.StockSymbol);
                    }
                }
                else
                {
                    await next();
                }
            }
            else
            {
                await next();
            }

            _logger.LogInformation("{FilterName}.{Action} - After", nameof(CreateOrderActionFilter), nameof(OnActionExecutionAsync));
        }
    }
}
