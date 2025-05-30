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
using api.Auth;
using System.Security.Claims;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class categoriesController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<categoriesController> _logger;
        private readonly ICategoryServices _categoryServices;

        private const string ERROR = "Quelque chose s'est mal passé !";
        private const string NOT_FOUND = "Catégorie introuvable !";

        public categoriesController(
            IConfiguration config,
            ILogger<categoriesController> logger,
            ICategoryServices categoryServices
        )
        {
            _config = config;
            _logger = logger;
            _categoryServices = categoryServices;
        }

        /// <summary>
        /// Get all vehicles categories
        /// </summary>
        /// <param name="subs"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("all")]
        public Task<IActionResult> GetAllCategories(SearchEntityDto<CategoryVehicleFilter> subs)
        {
            // get all categories
            var all = _categoryServices.getAll(subs);

            return Task.FromResult<IActionResult>(Ok(all));
        }

        /// <summary>
        /// Get Category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public Task<IActionResult> GetCategory(string id)
        {
            // get category by id
            var subs = _categoryServices.getDetail(id).Result;

            // return not found if null
            if (subs == null) return Task.FromResult<IActionResult>(NotFound(new
            {
                errorMsg = NOT_FOUND
            }));

            // return result
            return Task.FromResult<IActionResult>(Ok(subs));
        }

        /// <summary>
        /// Create vehicle category
        /// </summary>
        /// <param name="subs"></param>
        /// <returns></returns>
        [Authorize(Roles = $"{UserRoles.ADMIN}")]
        [HttpPost]
        public Task<IActionResult> PostCategory(CategoryVehicleDTO subs)
        {
            // get connected user id
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // assign connected user to entity
            subs.CreatedBy = userId ?? "";
            subs.UpdatedBy = userId ?? "";

            // create vehicle
            var cat = _categoryServices.createCategoryVehicle(subs).Result;

            // return error if null
            if (cat == null) return Task.FromResult<IActionResult>(BadRequest(new
            {
                errorMsg = ERROR
            }));

            return Task.FromResult<IActionResult>(Ok(cat));
        }
    }
}
