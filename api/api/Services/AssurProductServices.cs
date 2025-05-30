using api.Contexts;
using api.DTO;
using api.DTO.Filters;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<AssurProduct> createAssurProduct(AssurProductDTO product)
        {
            if (isExist(product.Name)) return null;

            // format object to create product
            var prd = new AssurProduct
            {
                Name = product.Name,
                Categories = product.Categories,
                Garanties = product.Garanties,
                CreatedBy = product.CreatedBy,
                UpdatedBy = product.UpdatedBy,
            };

            _context.AssurProducts.Add(prd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return null;
            }

            return prd;
        }

        // get All Assur Products
        public PagedResult<AssurProduct> getAll(SearchEntityDto<AssurProductFilter> search)
        {
            var query = _context.AssurProducts.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search.Filter?.Name))
            {
                query = query.Where(x => x.Name.Equals(search.Filter.Name));
            }

            var items = query.Skip(search.PageSize * search.PageIndex).Take(search.PageSize).ToList();
            
            return new PagedResult<AssurProduct>
            {
                Items = items,
                TotalItems = query.Count(),
                PageIndex = search.PageIndex,
                PageSize = search.PageSize
            };
        }

        // get Details Assur Product
        public async Task<AssurProduct> getDetail(string id)
        {
            return await _context.AssurProducts.FindAsync(id);
        }

        // check if Assur Product exist
        private bool isExist(string name)
        {
            return _context.AssurProducts.Any(e => e.Name == name);
        }
    }
}
