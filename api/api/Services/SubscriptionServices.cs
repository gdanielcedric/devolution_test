using api.Contexts;
using api.DTO;
using api.DTO.Filters;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace api.Services
{
    public class SubscriptionServices : ISubscriptionServices
    {
        private readonly ApiDbContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<SubscriptionServices> _logger;

        public SubscriptionServices(
            ApiDbContext context, 
            ILogger<SubscriptionServices> logger,
            IConfiguration config
        )
        {
            _context = context;
            _logger = logger;
            _config = config;
        }

        // Create Subscription
        public async Task<Subscription> createSubscription(Subscription subscription)
        {
            if (isExist(subscription.Id)) return null;

            _context.Subscriptions.Add(subscription);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return null;
            }

            return subscription;
        }

        // get All Subscriptions
        public PagedResult<VSSDTO> getAll(SearchEntityDto<SubscriptionFilter> search, string id = "")
        {
            var query = (from s in _context.Subscriptions
                         join sus in _context.Suscribers on s.IdSubscriber equals sus.Id
                         join v in _context.Vehicles on s.IdVehicle equals v.Id
                         join a in _context.AssurProducts on s.IdAssurProduct equals a.Id
                         select new VSSDTO
                         {
                             Id = s.Id,
                             QuoteReference = s.QuoteReference,
                             SuscriberNom = sus.Nom,
                             SuscriberPhone = sus.Telephone,
                             ImmatriculationNumber = v.ImmatriculationNumber,
                             AssurProduct = a.Name,
                             Price = s.Price,
                             CreatedAt = s.CreatedAt,
                             UpdatedAt = s.UpdatedAt,
                             CreatedBy = s.CreatedBy,
                             UpdatedBy = s.UpdatedBy,
                             Status = s.Status,
                             Step = s.Step,
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

        // get Details Subscription
        public async Task<Subscription> getDetail(string id)
        {
            return await _context.Subscriptions.FindAsync(id);
        }

        // check if subscription exist
        private bool isExist(string id)
        {
            return _context.Subscriptions.Any(e => e.Id == id);
        }
    }
}
