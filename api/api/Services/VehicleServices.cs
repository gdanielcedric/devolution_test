using api.Contexts;
using api.DTO;
using api.DTO.Filters;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

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

        // Create Vehicle
        public async Task<Vehicle> createVehicle(VehicleDTO vehicle, string id)
        {
            if (isExist(vehicle.Immatriculation)) return null;

            var voiture = new Vehicle
            {
                Couleur = vehicle.Couleur,
                DateFirstCirculation = vehicle.DateFirstUse,
                IdCategoryVehicle = vehicle.IdCategoryVehicle,
                NombreSiege = vehicle.SiegeNumber,
                NombrePorte = vehicle.PorteNumber,
                ImmatriculationNumber = vehicle.Immatriculation,
                IdSuscriber = id,
                ValueNeuve = vehicle.ValueNeuve,
                ValueVenale = vehicle.ValueVenale,
                FiscalPower = vehicle.FiscalPower,
                UpdatedBy = vehicle.UpdatedBy,
                CreatedBy = vehicle.CreatedBy,
            };

            _context.Vehicles.Add(voiture);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return null;
            }

            return voiture;
        }

        // get All Vehicles
        public PagedResult<Vehicle> getAll(SearchEntityDto<VehicleFilter> search)
        {
            var query = _context.Vehicles.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search.Filter?.ImmatriculationNumber))
            {
                query = query.Where(x => x.ImmatriculationNumber.Equals(search.Filter.ImmatriculationNumber));
            }

            var items = query.Skip(search.PageSize * search.PageIndex).Take(search.PageSize).ToList();

            return new PagedResult<Vehicle>
            {
                Items = items,
                TotalItems = query.Count(),
                PageIndex = search.PageIndex,
                PageSize = search.PageSize
            };
        }

        // get Vehicle Details
        public async Task<Vehicle> getDetail(string id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        // Check if Vehicle exist
        private bool isExist(string immatriculation)
        {
            return _context.Vehicles.Any(e => e.ImmatriculationNumber == immatriculation);
        }
    }
}
