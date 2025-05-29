using api.DTO.Filters;
using api.DTO;
using api.Models;

namespace api.Interfaces
{
    public interface ICategoryServices
    {
        public PagedResult<CategoryVehicle> getAll(SearchEntityDto<CategoryVehicleFilter> search);
        public Task<CategoryVehicle> getDetail(string id);
        public Task<CategoryVehicle> createCategoryVehicle(CategoryVehicleDTO vehicle);
    }
}
