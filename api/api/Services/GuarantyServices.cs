using api.Contexts;
using api.Interfaces;

namespace api.Services
{
    public class GuarantyServices : IGuarantyServices
    {
        private readonly ApiDbContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<GuarantyServices> _logger;

        public GuarantyServices(
            ApiDbContext context, 
            ILogger<GuarantyServices> logger, 
            IConfiguration config
        )
        {
            _context = context;
            _logger = logger;
            _config = config;
        }
    }
}
