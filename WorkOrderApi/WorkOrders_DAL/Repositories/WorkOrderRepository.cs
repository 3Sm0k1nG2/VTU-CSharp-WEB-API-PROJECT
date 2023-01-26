using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using WorkOrders_BAL.Dtos.WorkOrder;
using WorkOrders_BAL.Entities;
using WorkOrders_BAL.Interfaces;
using WorkOrders_DAL.DbContexts;
using WorkOrders_DAL.Interfaces.Mappers.Entity;
using WorkOrders_DAL.Interfaces.Repositories;
using WorkOrders_DAL.Repositories.Base;

namespace WorkOrders_DAL.Repositories
{
    public class WorkOrderRepository : RepositoryBase<WorkOrderEntity, WorkOrderDto, WorkOrderPatchDto>, IWorkOrderRepository
    {
        private new IWorkOrderMapper _mapper;

        public WorkOrderRepository(WorkOrderDbContext context, IWorkOrderMapper mapper, IMemoryCache memoryCache)
            : base(context, mapper, memoryCache)
        {
            _mapper = mapper;
        }

        public async virtual Task<IEnumerable<WorkOrderDto>> GetAll(
            uint page = 0,
            string? district = null,
            string? leadtech = null,
            string? service = null,
            string? payment = null
        )
        {
            if(page < 0)
            {
                page = 0;
            }

            const int ITEMS_PER_PAGE = 5;
            int items = (int) page * ITEMS_PER_PAGE; 
            var query = _context.Set<WorkOrderEntity>();

            return _mapper.Map(query
                .Skip(items)
                .Take(ITEMS_PER_PAGE)
                .ToList()
            );
        }

        public virtual IEnumerable<WorkOrderDto> GetRushJobs()
        {
            return _mapper.Map(_context.Set<WorkOrderEntity>().Where(x => x.Rush == true).ToList());
        }
        public virtual int GetRushJobsCount()
        {
            return _context.Set<WorkOrderEntity>().Where(x => x.Rush == true).Count();
        }

        public virtual decimal GetTotalCost()
        {
            return _context.Set<WorkOrderEntity>().Select(x => x.PartsCost).Sum();
        }
        public virtual decimal GetAverageCost()
        {
            return _context.Set<WorkOrderEntity>().Select(x => x.PartsCost).Average();
        }
        public virtual string GetMostFound(
            string col
        )
        {
            col = col.ToLower();

            Dictionary<Guid, int> counters = new Dictionary<Guid, int>();
            int bestCount = 0;
            Guid? bestId = null;

            switch (col)
            {
                case "district":
                    var districts = _context.Set<WorkOrderEntity>().Select(x => x.DistrictId).ToListAsync();
                    districts.Result.ForEach(x => {
                        if (counters.ContainsKey(x))
                        {
                            counters[x]++;
                        } else
                        {
                            counters[x] = 0;
                        }
                    });

                    foreach(var counter in counters)
                    {
                        if(counter.Value > bestCount)
                        {
                            bestId = counter.Key;
                            bestCount = counter.Value;
                        }
                    }

                    return _context.Districts.FirstAsync(x => x.Id == bestId).Result.Name;
                case "leadtech":
                    var leadtechs = _context.Set<WorkOrderEntity>().Select(x => x.LeadtechId).ToListAsync();
                    leadtechs.Result.ForEach(x => {
                        if (counters.ContainsKey(x))
                        {
                            counters[x]++;
                        }
                        else
                        {
                            counters[x] = 0;
                        }
                    });

                    foreach (var counter in counters)
                    {
                        if (counter.Value > bestCount)
                        {
                            bestId = counter.Key;
                            bestCount = counter.Value;
                        }
                    }

                    return _context.Leadtechs.FirstAsync(x => x.Id == bestId).Result.Name;
                case "service":
                    var services = _context.Set<WorkOrderEntity>().Select(x => x.ServiceId).ToListAsync();
                    services.Result.ForEach(x => {
                        if (counters.ContainsKey(x))
                        {
                            counters[x]++;
                        }
                        else
                        {
                            counters[x] = 0;
                        }
                    });

                    foreach (var counter in counters)
                    {
                        if (counter.Value > bestCount)
                        {
                            bestId = counter.Key;
                            bestCount = counter.Value;
                        }
                    }

                    return _context.Services.FirstAsync(x => x.Id == bestId).Result.Name;
                case "payment":

                    var payments = _context.Set<WorkOrderEntity>().Select(x => x.PaymentId).ToListAsync();
                    payments.Result.ForEach(x => {
                        if (counters.ContainsKey(x))
                        {
                            counters[x]++;
                        }
                        else
                        {
                            counters[x] = 0;
                        }
                    });

                    foreach (var counter in counters)
                    {
                        if (counter.Value > bestCount)
                        {
                            bestId = counter.Key;
                            bestCount = counter.Value;
                        }
                    }

                    return _context.Payments.FirstAsync(x => x.Id == bestId).Result.Name;
                default:
                    throw new Exception("no column " + col + " found in WorkOrder entity.");
            }
        }

        public async Task<WorkOrderDetailedDto> GetByIdDetailed(Guid id)
        {
            WorkOrderEntity? entity = await _context.Set<WorkOrderEntity>().FindAsync(id);

            if (entity == null)
            {
                throw new NullReferenceException(Constants.ERROR_ENTITY_NOT_FOUND);
            }

            return _mapper.MapDetailed(entity);
        }
    }
}
