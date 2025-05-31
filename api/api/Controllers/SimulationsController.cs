using api.Auth;
using api.DTO;
using api.DTO.Filters;
using api.Enums;
using api.Helpers;
using api.Interfaces;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/[controller]")]
    public class simulationsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAssurProductServices _assurProductServices;
        private readonly ISimulationServices _simulationsServices;
        private readonly IVehicleServices _vehicleServices;
        private readonly ILogger<simulationsController> _logger;

        private const string ERROR = "Quelque chose s'est mal passé !";
        private const string NOT_FOUND = "Simulation introuvable !";
        private const string NOT_FOUND_ASSUR = "Produiit d'assurance introuvable !";

        private Suscriber usertest = new Suscriber
        { 
            Address = "Abidjan",
            CNI = "CNI000000",
            Nom = "John",
            Prenom = "Doe",
            Ville = "Abidjan",
            Telephone = "0102030405"
        };

        public simulationsController(
            IConfiguration config,
            ILogger<simulationsController> logger,
            IAssurProductServices assurProductServices,
            ISimulationServices simulationsServices,
            IVehicleServices vehicleServices
        )
        {
            _config = config;
            _logger = logger;
            _assurProductServices = assurProductServices;
            _simulationsServices = simulationsServices;
            _vehicleServices = vehicleServices;
        }

        /// <summary>
        /// Get all simulations with filter option
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("all")]
        public Task<IActionResult> GetSimulations(SearchEntityDto<SimulationFilter> search)
        {
            string? idConnected = "";

            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin) idConnected = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // get all simulations
            var all = _simulationsServices.getAll(search, idConnected);

            return Task.FromResult<IActionResult>(Ok(all));
        }

        /// <summary>
        /// Get simulation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        [HttpGet("{id}")]
        public Task<IActionResult> GetSimulation(string id)
        {
            // get simulation by id
            var simul = _simulationsServices.getDetail(id).Result;

            // return not found if null
            if (simul == null) return Task.FromResult<IActionResult>(NotFound(new
            {
                errorMsg = NOT_FOUND
            }));

            // return result
            return Task.FromResult<IActionResult>(Ok(simul));
        }

        /// <summary>
        /// Create simulation
        /// </summary>
        /// <param name="simul"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<IActionResult> PostSimulation(SimulationDTO simul)
        {
            // get connected user id
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // assign connected user to entity
            simul.CreatedBy = userId ?? "";
            simul.UpdatedBy = userId ?? "";
            simul.Vehicle.CreatedBy = userId ?? "";
            simul.Vehicle.UpdatedBy = userId ?? "";

            // create vehicle
            var vehicule = _vehicleServices.createVehicle(simul.Vehicle, usertest.Id).Result;

            // check if vehicle creation is ok
            if (vehicule == null)
            {
                return Task.FromResult<IActionResult>(BadRequest(new
                {
                    errorMsg = ERROR
                }));
            }

            // get Assur Product
            var product = _assurProductServices.getDetail(simul.IdAssurProduct).Result;

            // check if assur product exist
            if (product == null)
            {
                return Task.FromResult<IActionResult>(NotFound(new
                {
                    errorMsg = NOT_FOUND_ASSUR
                }));
            }

            // get all guaranties in the product
            var guaranties = product.Garanties;

            // calculate prime for product
            var prime = Utility.CalculatePrime(vehicule, guaranties);

            // format object to create simulation
            var simulation = new Simulation
            {
                IdAssurProduct = simul.IdAssurProduct,
                IdVehicle = vehicule.Id,
                Price = prime,
                QuoteReference = $"QT{Utility.GenerateRandomString(12)}",
                CreatedBy = simul.CreatedBy,
                UpdatedBy = simul.UpdatedBy,
            };

            // create result
            var result = _simulationsServices.createSimulation(simulation).Result;

            // return error if null
            if (result == null) return Task.FromResult<IActionResult>(BadRequest(new
            {
                errorMsg = ERROR
            }));

            return Task.FromResult<IActionResult>(Ok(result));
        }
    }
}
