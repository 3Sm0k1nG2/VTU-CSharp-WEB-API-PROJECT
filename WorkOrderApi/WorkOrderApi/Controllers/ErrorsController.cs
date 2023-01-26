using Microsoft.AspNetCore.Mvc;

namespace WorkOrderApi.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error() => Problem();

    }
}
