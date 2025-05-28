using api.Contexts;
using api.Interfaces;

namespace api.Services
{
    public class SuscriberServices : ISuscriberServices
    {
        private readonly ApiDbContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<SuscriberServices> _logger;

        public SuscriberServices(
            ApiDbContext context, 
            ILogger<SuscriberServices> logger, 
            IConfiguration config
        )
        {
            _context = context;
            _logger = logger;
            _config = config;
        }
    }
}
