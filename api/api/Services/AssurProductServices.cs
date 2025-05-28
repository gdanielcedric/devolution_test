using api.Contexts;
using api.Interfaces;

namespace api.Services
{
    public class AssurProductServices : IAssurProductServices
    {
        private readonly ApiDbContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<AssurProductServices> _logger;

        public AssurProductServices(
            ApiDbContext context, 
            ILogger<AssurProductServices> logger, 
            IConfiguration config
        )
        {
            _context = context;
            _logger = logger;
            _config = config;
        }
    }
}
