using api.DTO;
using api.DTO.Filters;
using api.Models;

namespace api.Interfaces
{
    public interface IVehicleServices
    {
        public PagedResult<Vehicle> getAll(SearchEntityDto<VehicleFilter> search);
        public Task<Vehicle> getDetail(string id);
        public Task<Vehicle> createVehicle(VehicleDTO vehicle, string id);
    }
}
