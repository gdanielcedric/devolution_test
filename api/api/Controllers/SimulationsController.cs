using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class SimulationsController : ControllerBase
    {
        private readonly ILogger<SimulationsController> _logger;

        public SimulationsController(ILogger<SimulationsController> logger)
        {
            _logger = logger;
        }
    }
}
