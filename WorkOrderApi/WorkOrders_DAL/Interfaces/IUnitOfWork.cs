using WorkOrders_DAL.DbContexts;
using WorkOrders_DAL.Repositories;

namespace WorkOrders_DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // FIND ME
        // DELETE ME
        // Change to IDbContext, when it is available
        WorkOrderDbContext Context { get; }

        #region Repositories
        public WorkOrderRepository WorkOrder { get; }

        public DistrictRepository District { get; }
        public LeadtechRepository Leadtech { get;}
        public ServiceRepository Service { get; }
        public PaymentRepository Payment { get;  }
        public LaborRateRepository LaborRate { get; }
        public WeekdayRepository Weekday { get;  }
        #endregion

        void Save();
    }
}
