using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace StocksAppAssignment.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IHostEnvironment _environment;

        public ErrorController(IHostEnvironment environment)
        {
            _environment = environment;
        }

        [Route("/Error")]
        public IActionResult Error()
        {
            IExceptionHandlerPathFeature? exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if(exceptionHandlerPathFeature != null &&
               exceptionHandlerPathFeature.Error != null)
            {
                if (_environment.IsDevelopment())
                {
                    ViewBag.ErrorMessage = exceptionHandlerPathFeature.Error.Message;
                }
                else
                {
                    ViewBag.ErrorMessage = "An Error has occured. Please contact website administrator for assistance";
                }
            }

            return View();
        }
    }
}
