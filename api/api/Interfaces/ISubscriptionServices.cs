using api.DTO;
using api.DTO.Filters;
using api.Models;

namespace api.Interfaces
{
    public interface ISubscriptionServices
    {
        public PagedResult<VSSDTO> getAll(SearchEntityDto<SubscriptionFilter> search, string? id = "");
        public Task<Subscription> getDetail(string id);
        public Task<Subscription> createSubscription(Subscription subscription);
    }
}
