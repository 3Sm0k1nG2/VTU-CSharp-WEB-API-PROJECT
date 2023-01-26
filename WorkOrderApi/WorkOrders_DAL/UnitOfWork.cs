using Microsoft.Extensions.Caching.Memory;
using WorkOrders_DAL.DbContexts;
using WorkOrders_DAL.Interfaces;
using WorkOrders_DAL.Mappers.Repositories;
using WorkOrders_DAL.Repositories;

namespace WorkOrders_DAL
{
    public class UnitOfWork : IUnitOfWork
    {

        #region DbContext
        public WorkOrderDbContext Context { get; private set; }

        public WorkOrderRepository WorkOrder { get; private set; }

        public DistrictRepository District { get; private set; }
        public LeadtechRepository Leadtech { get; private set; }
        public ServiceRepository Service { get; private set; }
        public PaymentRepository Payment { get; private set; }
        public LaborRateRepository LaborRate { get; private set; }
        public WeekdayRepository Weekday { get; private set; }

        public UnitOfWork(WorkOrderDbContext context, IMemoryCache memoryCache)
        {
            Context = context;

            District = new DistrictRepository(Context, memoryCache);
            Leadtech = new LeadtechRepository(Context, memoryCache);
            Service = new ServiceRepository(Context, memoryCache);
            Payment = new PaymentRepository(Context, memoryCache);
            LaborRate = new LaborRateRepository(Context, memoryCache);
            Weekday = new WeekdayRepository(Context, memoryCache);

            WorkOrder = new WorkOrderRepository(Context, new WorkOrderMapper(new WorkOrderRequiredRepositories(this)), memoryCache);
        }
        #endregion

        #region Dispose
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Save
        public void Save()
        {
            Context.SaveChanges();
        }
        #endregion
    }
}
