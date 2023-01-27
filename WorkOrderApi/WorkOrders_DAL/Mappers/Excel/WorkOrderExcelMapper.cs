using System.Data;
using System.Reflection;
using WorkOrders_BAL.Dtos.WorkOrder;
using WorkOrders_BAL.Entities;
using WorkOrders_BAL.Interfaces;
using WorkOrders_DAL.Mappers.Excel.Base;

namespace WorkOrders_DAL.Mappers.Excel
{
    public class WorkOrderExcelMapper
    {
        protected static readonly Type _TYPE = typeof(WorkOrderDtoExcel);
        protected static readonly PropertyInfo[] _FIELDS = _TYPE.GetProperties().ToArray();

        public virtual WorkOrderDtoExcel MapFromDataRow(DataRow record)
        {
            var values = record.ItemArray;

            WorkOrderDtoExcel dto = new WorkOrderDtoExcel();

            for (int i = 0; i < _FIELDS.Length; i++)
            {
                var value = values[i];
                var field = _FIELDS[i];

                //if (field.Name == "Id")
                //    continue;

                PropertyInfo dtoProp = _TYPE.GetProperty(field.Name);

                if (value is DBNull || dtoProp == null)
                    continue;

                value = Utils.ChangeType(value, dtoProp.PropertyType);
                dtoProp.SetValue(dto, value);
            }

            return dto;
        }
    }
}