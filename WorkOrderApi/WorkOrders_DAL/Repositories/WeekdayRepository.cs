using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Data.Entity;
using WorkOrders_BAL.Dtos;
using WorkOrders_BAL.Entities;
using WorkOrders_BAL.Interfaces;
using WorkOrders_DAL.DbContexts;
using WorkOrders_DAL.Interfaces.Mappers.Entity.Base;
using WorkOrders_DAL.Repositories.Base;

namespace WorkOrders_DAL.Repositories
{
    public class WeekdayRepository : RepositoryBase<WeekdayEntity, WeekdayDto, WeekdayDto>
    {

        public WeekdayRepository(WorkOrderDbContext context, IMemoryCache memoryCache) : base(context, memoryCache) { }

        public virtual IEnumerable<object> GetAll(
            string? column,
            string? day
        )
        {
            var query = _context.Set<WeekdayEntity>();

            day = day?.ToLower();
            column = column?.ToLower();

            if (day != null && column == "name")
            {
                return query.Where(x => x.Name.ToLower().StartsWith(day)).Select(x => x.Name).ToList();
            }

            if (day != null)
            {
                return this._mapper.Map(query.Where(x => x.Name.ToLower().StartsWith(day)).ToList());
            }

            if (column == "name")
            {
                return query.Select(x => x.Name).ToList();
            }

            var data = _memoryCache.Get<IEnumerable<object>>(Cache.Keys.Weekdays);

            if (data is null)
            {
                data = this._mapper.Map(query.ToList());
                _memoryCache.Set(Cache.Keys.Weekdays, data, Cache.Options.TimeSpans.Day);
            }

            return data;
        }

    }


}
