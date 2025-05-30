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
using api.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class productsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<productsController> _logger;
        private readonly IAssurProductServices _assurProductServices;

        private const string ERROR = "Quelque chose s'est mal passé !";
        private const string NOT_FOUND = "Produit d'assurance introuvable !";

        public productsController(
            IConfiguration config,
            ILogger<productsController> logger,
            IAssurProductServices assurProductServices
        )
        {
            _config = config;
            _logger = logger;
            _assurProductServices = assurProductServices;
        }

        /// <summary>
        /// Get all assur products
        /// </summary>
        /// <param name="subs"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("all")]
        public Task<IActionResult> GetAllAssurProducts(SearchEntityDto<AssurProductFilter> subs)
        {
            // get all assur products
            var all = _assurProductServices.getAll(subs);

            return Task.FromResult<IActionResult>(Ok(all));
        }

        /// <summary>
        /// Get Product assur by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public Task<IActionResult> GetAssurProduct(string id)
        {
            // get category by id
            var subs = _assurProductServices.getDetail(id).Result;

            // return not found if null
            if (subs == null) return Task.FromResult<IActionResult>(NotFound(new
            {
                errorMsg = NOT_FOUND
            }));

            // return result
            return Task.FromResult<IActionResult>(Ok(subs));
        }

        /// <summary>
        /// Create assur product
        /// </summary>
        /// <param name="subs"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public Task<IActionResult> PostAssurProduct(AssurProductDTO subs)
        {
            // get connected user id
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // assign connected user to entity
            subs.CreatedBy = userId ?? "";
            subs.UpdatedBy = userId ?? "";

            // create vehicle
            var cat = _assurProductServices.createAssurProduct(subs).Result;

            // return error if null
            if (cat == null) return Task.FromResult<IActionResult>(BadRequest(new
            {
                errorMsg = ERROR
            }));

            return Task.FromResult<IActionResult>(Ok(cat));
        }
    }
}
