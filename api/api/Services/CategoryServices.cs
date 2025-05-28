using api.Contexts;
using api.Interfaces;

namespace api.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ApiDbContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<CategoryServices> _logger;

        public CategoryServices(
            ApiDbContext context, 
            ILogger<CategoryServices> logger, 
            IConfiguration config
        )
        {
            _context = context;
            _logger = logger;
            _config = config;
        }
    }
}
