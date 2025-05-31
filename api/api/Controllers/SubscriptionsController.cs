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
using api.Auth;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/[controller]")]
    public class subscriptionsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<subscriptionsController> _logger;
        private readonly IAssurProductServices _assurProductServices;
        private readonly ISubscriptionServices _subscriptionsServices;
        private readonly ISuscriberServices _suscriberServices;
        private readonly IVehicleServices _vehicleServices;

        private const string BAD_PHONE = "Numero de telephone incorrect !";
        private const string ERROR = "Quelque chose s'est mal passé !";
        private const string NOT_FOUND = "Souscription introuvable !";
        private const string NOT_FOUND_ASSUR = "Produiit d'assurance introuvable !";

        public subscriptionsController(
            IConfiguration config,
            ILogger<subscriptionsController> logger,
            IAssurProductServices assurProductServices,
            ISubscriptionServices subscriptionsServices,
            ISuscriberServices suscriberServices,
            IVehicleServices vehicleServices
        )
        {
            _config = config;
            _logger = logger;
            _assurProductServices = assurProductServices;
            _subscriptionsServices = subscriptionsServices;
            _suscriberServices = suscriberServices;
            _vehicleServices = vehicleServices;
        }

        /// <summary>
        /// Get all subscriptions with filter option
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("all")]
        public Task<IActionResult> GetAllSubscription(SearchEntityDto<SubscriptionFilter> search)
        {
            string? idConnected = "";

            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin) idConnected = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // get all subscriptions
            var all = _subscriptionsServices.getAll(search, idConnected);

            return Task.FromResult<IActionResult>(Ok(all));
        }

        /// <summary>
        /// Get subscription by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<IActionResult> GetSubscription(string id)
        {
            // get subscription by id
            var subs = _subscriptionsServices.getDetail(id).Result;

            // return not found if null
            if (subs == null) return Task.FromResult<IActionResult>(NotFound(new
            {
                errorMsg = NOT_FOUND
            }));

            // return result
            return Task.FromResult<IActionResult>(Ok(subs));
        }

        /// <summary>
        /// Get status subscription by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("status/{id}")]
        public Task<IActionResult> GetSubscriptionStatus(string id)
        {
            // get subscription by id
            var subs = _subscriptionsServices.getDetail(id).Result;

            // return not found if null
            if (subs == null) return Task.FromResult<IActionResult>(NotFound(new
            {
                errorMsg = NOT_FOUND
            }));

            // return result
            return Task.FromResult<IActionResult>(Ok(subs.Step));
        }

        /// <summary>
        /// Get attestation subscription by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}/attestation")]
        public IActionResult GetSubscriptionAttestation(string id)
        {
            // get subscription by id
            var subs = _subscriptionsServices.getDetail(id).Result;

            // return not found if null
            if (subs == null) return NotFound(new
            {
                errorMsg = NOT_FOUND
            });

            // get vehicle
            var vehicle = _vehicleServices.getDetail(subs.IdVehicle).Result;

            // get Assure
            var suscriber = _suscriberServices.getDetail(subs.IdSubscriber).Result;

            // get Produc Assur
            var assur = _assurProductServices.getDetail(subs.IdAssurProduct).Result;

            // generate PDF
            var html = Utility.GeneratePDF(subs, suscriber, vehicle, assur);

            // formatter le nom du fichier
            var fileName = $"{Utility.GetTimestamp(DateTime.Now)}.pdf";

            //
            var memoryStream = new MemoryStream();

            // Générer le PDF directement en mémoire
            iText.Html2pdf.HtmlConverter.ConvertToPdf(html, memoryStream);

            var bytes = memoryStream.ToArray(); // ici on copie les données

            return File(bytes, "application/pdf", fileName); // plus besoin du stream ensuite

        }

        /// <summary>
        /// Create subscription
        /// </summary>
        /// <param name="subs"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<IActionResult> PostSubscription(SubscriptionDTO subs)
        {
            // get connected user id
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // assign connected user to entity
            subs.CreatedBy = userId ?? "";
            subs.UpdatedBy = userId ?? "";
            subs.Suscriber.CreatedBy = userId ?? "";
            subs.Suscriber.UpdatedBy = userId ?? "";
            subs.Vehicle.CreatedBy = userId ?? "";
            subs.Vehicle.UpdatedBy = userId ?? "";

            // verif phone
            if (!Utility.isGoodPhone(subs.Suscriber.Telephone))
            {
                return Task.FromResult<IActionResult>(BadRequest(new
                {
                    errorMsg = BAD_PHONE
                }));
            }

            // create suscriber
            var suscriber = _suscriberServices.createSuscriber(subs.Suscriber).Result;

            // check if suscriber creation is ok
            if(suscriber == null) 
            {
                return Task.FromResult<IActionResult>(BadRequest(new
                {
                    errorMsg = ERROR
                }));
            }

            // create vehicle
            var vehicule = _vehicleServices.createVehicle(subs.Vehicle, suscriber.Id).Result;

            // check if vehicle creation is ok
            if (vehicule == null)
            {
                return Task.FromResult<IActionResult>(BadRequest(new
                {
                    errorMsg = ERROR
                }));
            }

            // get Assur Product
            var product = _assurProductServices.getDetail(subs.IdAssurProduct).Result;

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

            // format object to create subscription
            var subscription = new Subscription
            {
                IdAssurProduct = subs.IdAssurProduct,
                IdSubscriber = suscriber.Id,
                IdVehicle = vehicule.Id,
                Price = prime,
                Step = ClientEnum.PROSPECT.Humanize(),
                QuoteReference = $"QT{Utility.GenerateRandomString(12)}",
                CreatedBy = subs.CreatedBy,
                UpdatedBy = subs.UpdatedBy,
            };

            // create result
            var result = _subscriptionsServices.createSubscription(subscription).Result;

            // return error if null
            if (result == null) return Task.FromResult<IActionResult>(BadRequest(new
            {
                errorMsg = ERROR
            }));

            return Task.FromResult<IActionResult>(Ok(result));
        }
    }
}
