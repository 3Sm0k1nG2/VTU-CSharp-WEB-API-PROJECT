using WorkOrders_BAL.Dtos.WorkOrder;
using WorkOrders_BAL.Entities;
using WorkOrders_DAL.Interfaces.Mappers.Excel.Base;

namespace WorkOrders_DAL.Interfaces.Mappers.Excel
{
    public interface IWorkOrderExcelMapper : IExcelMapper<WorkOrderEntity, WorkOrderDto>
    {

    }
}
