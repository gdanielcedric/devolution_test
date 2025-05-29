using api.DTO;
using api.DTO.Filters;
using api.Models;

namespace api.Interfaces
{
    public interface IAssurProductServices
    {
        public PagedResult<AssurProduct> getAll(SearchEntityDto<AssurProductFilter> search);
        public Task<AssurProduct> getDetail(string id);
        public Task<AssurProduct> createAssurProduct(AssurProductDTO product);
    }
}
