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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/[controller]")]
    public class suscribersController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<suscribersController> _logger;
        private readonly ISuscriberServices _suscriberServices;

        private const string ERROR = "Quelque chose s'est mal passé !";
        private const string NOT_FOUND = "Suscriber introuvable !";

        public suscribersController(
            IConfiguration config,
            ILogger<suscribersController> logger,
            ISuscriberServices suscriberServices
        )
        {
            _config = config;
            _logger = logger;
            _suscriberServices = suscriberServices;
        }

        /// <summary>
        /// Get all suscribers
        /// </summary>
        /// <param name="subs"></param>
        /// <returns></returns>
        [HttpPost("all")]
        public Task<IActionResult> GetAllSuscribers(SearchEntityDto<SuscriberFilter> subs)
        {
            string? idConnected = "";

            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin) idConnected = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // get all suscribers
            var all = _suscriberServices.getAll(subs, idConnected);

            return Task.FromResult<IActionResult>(Ok(all));
        }

        /// <summary>
        /// Get suscriber by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<IActionResult> GetDetailSuscriber(string id)
        {
            // get suscriber by id
            var subs = _suscriberServices.getDetail(id).Result;

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
