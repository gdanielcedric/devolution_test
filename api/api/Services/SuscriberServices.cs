using api.Contexts;
using api.DTO;
using api.DTO.Filters;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

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

        // Create Suscriber
        public async Task<Suscriber> createSuscriber(SuscriberDTO suscriber)
        {
            if (isExist(suscriber.Telephone)) return null;

            var susb = new Suscriber
            {
                Telephone = suscriber.Telephone,
                Address = suscriber.Address,
                CNI = suscriber.Cni,
                Nom = suscriber.Nom,
                Prenom = suscriber.Prenom,
                Ville = suscriber.Ville
            };

            _context.Suscribers.Add(susb);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return null;
            }

            return susb;
        }

        // get All Suscribers
        public PagedResult<Suscriber> getAll(SearchEntityDto<SuscriberFilter> search)
        {
            var query = _context.Suscribers.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search.Filter?.CNI))
            {
                query = query.Where(x => x.CNI.Equals(search.Filter.CNI));
            }

            if (!string.IsNullOrWhiteSpace(search.Filter?.Telephone))
            {
                query = query.Where(x => x.Telephone.Equals(search.Filter.Telephone));
            }

            var items = query.Skip(search.PageSize * search.PageIndex).Take(search.PageSize).ToList();

            return new PagedResult<Suscriber>
            {
                Items = items,
                TotalItems = query.Count(),
                PageIndex = search.PageIndex,
                PageSize = search.PageSize
            };
        }

        // get Suscriber Details
        public async Task<Suscriber> getDetail(string telephone)
        {
            return await _context.Suscribers.FindAsync(telephone);
        }

        // Check if suscriber exist
        private bool isExist(string telephone)
        {
            return _context.Suscribers.Any(e => e.Telephone == telephone);
        }
    }
}
