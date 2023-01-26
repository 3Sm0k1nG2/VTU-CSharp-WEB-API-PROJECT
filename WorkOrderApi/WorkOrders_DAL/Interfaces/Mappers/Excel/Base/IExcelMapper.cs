using System.Data;
using WorkOrders_BAL.Interfaces;
using WorkOrders_DAL.Interfaces.Mappers.Entity.Base;

namespace WorkOrders_DAL.Interfaces.Mappers.Excel.Base
{
    public interface IExcelMapper<TEntity, TDto> : IMapper<TEntity, TDto>
        where TEntity : IEntity
        where TDto : IDto
    {
        TDto MapFromDataRow(DataRow values);
    }
}
