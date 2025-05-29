using api.DTO;
using api.DTO.Filters;
using api.Models;

namespace api.Interfaces
{
    public interface ISuscriberServices
    {
        public PagedResult<Suscriber> getAll(SearchEntityDto<SuscriberFilter> search);
        public Task<Suscriber> getDetail(string id);
        public Task<Suscriber> createSuscriber(SuscriberDTO suscriber);
    }
}
