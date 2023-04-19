using Microsoft.AspNetCore.Mvc;

namespace StocksAppAssignment.UI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseController: ControllerBase
    {
    }
}
