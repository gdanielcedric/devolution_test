using api.DTO;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class subscriptionsController : ControllerBase
    {
        private readonly ILogger<subscriptionsController> _logger;

        public subscriptionsController(ILogger<subscriptionsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public Task<IActionResult> GetSubscription(string id)
        {
            return Task.FromResult<IActionResult>(Ok());
        }

        [HttpGet("status/{id}")]
        public Task<IActionResult> GetSubscriptionStatus(string id)
        {
            return Task.FromResult<IActionResult>(Ok());
        }

        [HttpGet("{id}/attestation")]
        public Task<IActionResult> GetSubscriptionAttestation(string id)
        {
            return Task.FromResult<IActionResult>(Ok());
        }

        [HttpPost]
        public Task<IActionResult> PostSubscription(SubscriptionDTO subs)
        {
            //return CreatedAtAction("GetSimulation", new { id = ""}, "");
            return Task.FromResult<IActionResult>(Ok());
        }
    }
}
