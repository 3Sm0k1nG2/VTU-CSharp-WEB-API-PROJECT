using WorkOrders_BAL.Dtos.WorkOrder;
using WorkOrders_BAL.Entities;

namespace WorkOrders_DAL.Interfaces.Repositories
{
    public interface IWorkOrderRepository : IRepository<WorkOrderEntity, WorkOrderDto, WorkOrderPatchDto>
    {
        Task<WorkOrderDetailedDto> GetByIdDetailed(Guid id);
    }
}
