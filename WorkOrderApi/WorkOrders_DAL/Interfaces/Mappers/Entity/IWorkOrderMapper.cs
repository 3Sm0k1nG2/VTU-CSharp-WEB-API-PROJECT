using WorkOrders_BAL.Dtos.WorkOrder;
using WorkOrders_BAL.Entities;
using WorkOrders_DAL.Interfaces.Mappers.Entity.Base;

namespace WorkOrders_DAL.Interfaces.Mappers.Entity
{
    public interface IWorkOrderMapper : IEntityMapper<WorkOrderEntity, WorkOrderDto, WorkOrderPatchDto>
    {
        WorkOrderDetailedDto MapDetailed(WorkOrderEntity entity);
    }
}
