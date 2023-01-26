using System.Data;
using System.Reflection;
using WorkOrders_BAL.Interfaces;
using WorkOrders_DAL.Interfaces.Mappers.Excel.Base;
using WorkOrders_DAL.Mappers.Base;

namespace WorkOrders_DAL.Mappers.Excel.Base
{
    public class ExcelMapper<TEntity, TDto> : Mapper<TEntity, TDto>, IExcelMapper<TEntity, TDto>
        where TEntity : class, IEntity, new()
        where TDto : class, IDto, new()
    {
        protected static readonly Type _TYPE = typeof(TDto);
        protected static readonly PropertyInfo[] _FIELDS = _TYPE.GetProperties().ToArray();

        public virtual TDto MapFromDataRow(DataRow record)
        {
            var values = record.ItemArray;

            TDto dto = new TDto();

            for (int i = 0; i < values.Length; i++)
            {
                var value = values[i];
                var field = _FIELDS[i];

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
