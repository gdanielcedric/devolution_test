using api.Contexts;
using api.DTO;
using api.DTO.Filters;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

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

        // Create Category Vehicle
        public async Task<CategoryVehicle> createCategoryVehicle(CategoryVehicleDTO vehicleDto)
        {
            if (isExist(vehicleDto.Code)) return null;

            // format object to create vehicle
            var vehicle = new CategoryVehicle
            {
                Code = vehicleDto.Code,
                Libelle = vehicleDto.Libelle,
                Description = vehicleDto.Description,
                CreatedBy = vehicleDto.CreatedBy,
                UpdatedBy = vehicleDto.UpdatedBy,
            };

            _context.CategoryVehicles.Add(vehicle);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return null;
            }

            return vehicle;
        }

        // get All Categories Vehicles
        public PagedResult<CategoryVehicle> getAll(SearchEntityDto<CategoryVehicleFilter> search)
        {
            var query = _context.CategoryVehicles.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search.Filter?.Code))
            {
                query = query.Where(x => x.Code.Equals(search.Filter.Code));
            }

            if (!string.IsNullOrWhiteSpace(search.Filter?.Libelle))
            {
                query = query.Where(x => x.Libelle.Equals(search.Filter.Libelle));
            }

            var items = query.Skip(search.PageSize * search.PageIndex).Take(search.PageSize).ToList();

            return new PagedResult<CategoryVehicle>
            {
                Items = items,
                TotalItems = query.Count(),
                PageIndex = search.PageIndex,
                PageSize = search.PageSize
            };
        }

        // get Detail Category Vehicle
        public async Task<CategoryVehicle> getDetail(string id)
        {
            return await _context.CategoryVehicles.FindAsync(id);
        }

        // check if Category Vehicle exist
        private bool isExist(string code)
        {
            return _context.CategoryVehicles.Any(e => e.Code == code);
        }
    }
}
