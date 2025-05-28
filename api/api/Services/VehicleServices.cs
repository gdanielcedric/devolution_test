using api.Contexts;
using api.Interfaces;

namespace api.Services
{
    public class VehicleServices : IVehicleServices
    {
        private readonly ApiDbContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<VehicleServices> _logger;

        public VehicleServices(
            ApiDbContext context, 
            ILogger<VehicleServices> logger, 
            IConfiguration config
        ) 
        { 
            _context = context;
            _logger = logger;
            _config = config;
        }
    }
}
