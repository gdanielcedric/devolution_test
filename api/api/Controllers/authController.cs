using api.DTO;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class authController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<authController> _logger;

        private const string ERROR = "Quelque chose s'est mal passé !";
        private const string NOT_FOUND = "Utilisateur introuvable !";

        public authController
        (
            IConfiguration config,
            IHttpClientFactory httpClientFactory,
            ILogger<authController> logger
        )
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthDTO request)
        {
            var client = _httpClientFactory.CreateClient();

            var url = $"{_config["Keycloack:Url"]}";
            var clientId = _config["Keycloack:ClientId"];
            var clientSecret = _config["Keycloack:ClientSecret"];

            var content = new StringContent(
                $"grant_type=password&client_id={clientId}&client_secret={clientSecret}&username={request.Username}&password={request.Password}",
                Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return BadRequest(new { error = "Login failed", details = error });
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            return Content(responseBody, "application/json");
        }
    }
}
