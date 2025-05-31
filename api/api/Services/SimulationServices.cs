using api.Contexts;
using api.DTO;
using api.DTO.Filters;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace api.Services
{
    public class SimulationServices : ISimulationServices
    {
        private readonly ApiDbContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<SimulationServices> _logger;

        public SimulationServices(
            ApiDbContext context, 
            ILogger<SimulationServices> logger,
            IConfiguration config
        )
        {
            _context = context;
            _logger = logger;
            _config = config;
        }

        // Create Simulation
        public async Task<Simulation> createSimulation(Simulation simulation)
        {
            if (isExist(simulation.Id)) return null;

            _context.Simulations.Add(simulation);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return null;
            }

            return simulation;
        }

        // get All Simulations
        public PagedResult<VSSDTO> getAll(SearchEntityDto<SimulationFilter> search, string id = "")
        {
            var query = (from s in _context.Simulations
                         join v in _context.Vehicles on s.IdVehicle equals v.Id
                         join a in _context.AssurProducts on s.IdAssurProduct equals a.Id
                         select new VSSDTO
                         {
                             Id = s.Id,
                             QuoteReference = s.QuoteReference,
                             ImmatriculationNumber = v.ImmatriculationNumber,
                             AssurProduct = a.Name,
                             Price = s.Price,
                             CreatedAt = s.CreatedAt,
                             UpdatedAt = s.UpdatedAt,
                             CreatedBy = s.CreatedBy,
                             UpdatedBy = s.UpdatedBy,
                             Status = s.Status
                         }).AsEnumerable();

            // if id not null, return suscribers for this user
            if (!string.IsNullOrWhiteSpace(id))
            {
                query = query.Where(x => x.CreatedBy.Equals(id));
            }

            if (!string.IsNullOrWhiteSpace(search.Filter?.quoteReference))
            {
                query = query.Where(x => x.QuoteReference.Equals(search.Filter.quoteReference));
            }

            var items = query.Skip(search.PageSize * search.PageIndex).Take(search.PageSize).ToList();

            return new PagedResult<VSSDTO>
            {
                Items = items,
                TotalItems = query.Count(),
                PageIndex = search.PageIndex,
                PageSize = search.PageSize
            };
        }

        // get Details Simulation
        public async Task<Simulation> getDetail(string id)
        {
            return await _context.Simulations.FindAsync(id);
        }

        // check if simulation exist
        private bool isExist(string id)
        {
            return _context.Simulations.Any(e => e.Id == id);
        }
    }
}
