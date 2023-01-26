using Microsoft.Extensions.Caching.Memory;
using WorkOrders_BAL.Dtos;
using WorkOrders_BAL.Entities;
using WorkOrders_DAL.DbContexts;
using WorkOrders_DAL.Repositories.Base;

namespace WorkOrders_DAL.Repositories
{
    public class LaborRateRepository : RepositoryBase<LaborRateEntity, LaborRateDto, LaborRateDto>
    {
        public LaborRateRepository(WorkOrderDbContext context, IMemoryCache memoryCache) : base(context, memoryCache) { }

        public virtual IEnumerable<object> GetAll(
            uint? techsMax,
            uint? rateMax,
            uint? techsMin = 0,
            uint? rateMin = 0
        )
        {
            var query = _context.Set<LaborRateEntity>();

            if (techsMax != null && rateMax != null)
            {
                return this._mapper.Map(query
                    .Where(x => x.TechsCount >= techsMin && x.TechsCount <= techsMax)
                    .Where(x => x.LbrRate >= rateMin && x.LbrRate <= rateMax)
                    .ToList());
            }

            if (techsMax.HasValue && rateMax == null)
            {
                return this._mapper.Map(query
                    .Where(x => x.LbrRate >= rateMin)
                    .Where(x => x.TechsCount >= techsMin && x.TechsCount <= techsMax)
                    .ToList()
                );
            }

            if (rateMax.HasValue)
            {
                return this._mapper.Map(query
                    .Where(x => x.TechsCount >= techsMin)
                    .Where(x => x.LbrRate >= rateMin && x.LbrRate <= rateMax)
                    .ToList()
                );
            }

            var data = _memoryCache.Get<IEnumerable<object>>(Cache.Keys.LaborRate);

            if (data is null)
            {
                data = this._mapper.Map(query.ToList());
                _memoryCache.Set(Cache.Keys.LaborRate, data, Cache.Options.TimeSpans.Day);
            }

            return data;
        }
    }
}
