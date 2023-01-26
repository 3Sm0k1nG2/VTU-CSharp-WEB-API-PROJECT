﻿using Microsoft.Extensions.Caching.Memory;
using WorkOrders_BAL.Dtos;
using WorkOrders_BAL.Entities;
using WorkOrders_DAL.DbContexts;
using WorkOrders_DAL.Repositories.Base;

namespace WorkOrders_DAL.Repositories
{
    public class DistrictRepository : RepositoryBase<DistrictEntity, DistrictDto, DistrictDto>
    {
        public DistrictRepository(WorkOrderDbContext context, IMemoryCache memoryCache) : base(context, memoryCache) { }

        public virtual IEnumerable<object> GetAll(
            string? column,
            string? name
        )
        {
            var query = _context.Set<DistrictEntity>();

            name = name?.ToLower();
            column = column?.ToLower();

            if (name != null && column == "name")
            {
                return query.Where(x => x.Name.ToLower().StartsWith(name)).Select(x => x.Name).ToList();
            }

            if (name != null)
            {
                return this._mapper.Map(query.Where(x => x.Name.ToLower().StartsWith(name)).ToList());
            }

            if (column == "name")
            {
                return query.Select(x => x.Name).ToList();
            }

            var data = _memoryCache.Get<IEnumerable<object>>(Cache.Keys.Districts);

            if(data is null)
            {
                data = this._mapper.Map(query.ToList());
                _memoryCache.Set(Cache.Keys.Districts, data, Cache.Options.TimeSpans.Day);
            }

            return data;
        }
    }
}
