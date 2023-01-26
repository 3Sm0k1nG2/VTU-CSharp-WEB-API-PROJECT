using WorkOrders_DAL.Interfaces;
using WorkOrders_DAL.Repositories;

namespace WorkOrders_DAL
{
    public class WorkOrderRequiredRepositories
    {
        public DistrictRepository District { get; }
        public LeadtechRepository Leadtech { get; }
        public ServiceRepository Service { get; }
        public PaymentRepository Payment { get; }
        public LaborRateRepository LaborRate { get; }
        public WeekdayRepository Weekday { get; }

        public WorkOrderRequiredRepositories(IUnitOfWork unitOfWork)
        {
            District = unitOfWork.District;
            Leadtech = unitOfWork.Leadtech;
            Service = unitOfWork.Service;
            Payment = unitOfWork.Payment;
            LaborRate = unitOfWork.LaborRate;
            Weekday = unitOfWork.Weekday;
        }
    }
}
