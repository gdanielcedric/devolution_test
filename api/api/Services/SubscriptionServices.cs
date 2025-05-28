using api.Contexts;
using api.Interfaces;

namespace api.Services
{
    public class SubscriptionServices : ISubscriptionServices
    {
        private readonly ApiDbContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<SubscriptionServices> _logger;

        public SubscriptionServices(
            ApiDbContext context, 
            ILogger<SubscriptionServices> logger,
            IConfiguration config
        )
        {
            _context = context;
            _logger = logger;
            _config = config;
        }
    }
}
