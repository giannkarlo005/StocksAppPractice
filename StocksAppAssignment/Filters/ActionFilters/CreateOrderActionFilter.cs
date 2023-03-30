using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ServiceContracts.DTO;
using StocksAppAssignment.Controllers;
using StocksAppAssignment.Models;

namespace StocksAppAssignment.Filters.ActionFilters
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
                    if (context.ActionArguments.ContainsKey("buyOrderRequest"))
                    {
                        OrderRequest buyOrderRequest = context.ActionArguments["buyOrderRequest"] as OrderRequest;

                        _logger.LogError("Buy Order Quantity is {OrderQuantity}", Convert.ToString(buyOrderRequest.OrderQuantity));
                        _logger.LogError("Buy Order Price is {OrderPrice}", Convert.ToString(buyOrderRequest.OrderQuantity));
                        _logger.LogError("Buy Order Stock Symbol is null");
                        _logger.LogError("Buy Order Date and Time of Order is {DateAndTimeOfOrder}", Convert.ToString(buyOrderRequest.DateAndTimeOfOrder));

                        tradeController.ViewBag.Errors = tradeController.ModelState.Values.SelectMany(x => x.Errors).Select(y => y.ErrorMessage).ToList();
                        context.Result = await tradeController.Index(buyOrderRequest.StockSymbol);
                    }
                    else if (context.ActionArguments.ContainsKey("sellOrderRequest"))
                    {
                        OrderRequest sellOrderRequest = context.ActionArguments["sellOrderRequest"] as OrderRequest;

                        _logger.LogError("Sell Order Quantity is {OrderQuantity}", Convert.ToString(sellOrderRequest.OrderQuantity));
                        _logger.LogError("Sell Order Price is {OrderPrice}", Convert.ToString(sellOrderRequest.OrderQuantity));
                        _logger.LogError("Sell Order Stock Symbol is null");
                        _logger.LogError("Sell Order Date and Time of Order is {DateAndTimeOfOrder}", Convert.ToString(sellOrderRequest.DateAndTimeOfOrder));

                        tradeController.ViewBag.Errors = tradeController.ModelState.Values.SelectMany(x => x.Errors).Select(y => y.ErrorMessage).ToList();
                        context.Result = await tradeController.Index(sellOrderRequest.StockSymbol);
                    }
                    else
                    {
                        tradeController.ViewBag.Error = "OrderRequest is Invalid";
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
