using api.DTO;
using api.DTO.Filters;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using System.IO;
using Microsoft.AspNetCore.Http.HttpResults;
using api.Enums;
using Humanizer;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class vehiclesController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<vehiclesController> _logger;
        private readonly IVehicleServices _vehicleServices;

        private const string ERROR = "Quelque chose s'est mal passé !";
        private const string NOT_FOUND = "Suscriber introuvable !";

        public vehiclesController(
            IConfiguration config,
            ILogger<vehiclesController> logger,
            IVehicleServices vehicleServices
        )
        {
            _config = config;
            _logger = logger;
            _vehicleServices = vehicleServices;
        }

        /// <summary>
        /// Get all vehicles
        /// </summary>
        /// <param name="subs"></param>
        /// <returns></returns>
        [HttpPost("all")]
        public Task<IActionResult> GetAllVehicles(SearchEntityDto<VehicleFilter> subs)
        {
            // get all vehicles
            var all = _vehicleServices.getAll(subs);

            return Task.FromResult<IActionResult>(Ok(all));
        }

        /// <summary>
        /// Get vehicle by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<IActionResult> GetVehicle(string id)
        {
            // get vehicle by id
            var subs = _vehicleServices.getDetail(id).Result;

            // return not found if null
            if (subs == null) return Task.FromResult<IActionResult>(NotFound(new
            {
                errorMsg = NOT_FOUND
            }));

            // return result
            return Task.FromResult<IActionResult>(Ok(subs));
        }
    }
}
