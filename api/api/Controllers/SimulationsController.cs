using api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class simulationsController : ControllerBase
    {
        private readonly ILogger<simulationsController> _logger;

        public simulationsController(ILogger<simulationsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Task<IActionResult> GetSimulations()
        {
            return Task.FromResult<IActionResult>(Ok());
        }

        [HttpGet("{id}")]
        public Task<IActionResult> GetSimulation(string id)
        {
            return Task.FromResult<IActionResult>(Ok());
        }

        [HttpPost]
        public Task<IActionResult> PostSimulation(SimulationDTO simul)
        {
            //return CreatedAtAction("GetSimulation", new { id = ""}, "");
            return Task.FromResult<IActionResult>(Ok());
        }
    }
}
