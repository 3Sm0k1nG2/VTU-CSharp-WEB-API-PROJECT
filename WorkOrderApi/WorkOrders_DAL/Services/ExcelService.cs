using ExcelDataReader;
using WorkOrders_BAL.Interfaces;
using WorkOrders_DAL.DbContexts;
using WorkOrders_DAL.Interfaces.Mappers.Excel.Base;
using WorkOrders_DAL.Interfaces.Services;

namespace WorkOrders_DAL.Services.Excel
{
    public class ExcelService<TEntity, TDto> : IExcelService<TDto>
        where TEntity : class, IEntity, new()
        where TDto : class, IDto, new()
    {
        protected WorkOrderDbContext _context;
        protected IExcelMapper<TEntity, TDto> _mapper;

        public ExcelService(WorkOrderDbContext context, IExcelMapper<TEntity, TDto> mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IList<TDto> Read(string filePath)
        {
            IList<TDto> dtos = new List<TDto>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    const int TABLE_INDEX = 0;
                    const int FIELDS_ROW_INDEX = 0;

                    var result = reader.AsDataSet();
                    var table = result.Tables[TABLE_INDEX];
                    var fieldsRow = table.Rows[FIELDS_ROW_INDEX].ItemArray;

                    // Read records and save them to order list.
                    //
                    // If it were starting from 1, it would have been named 'lastRow',  ( i <= lastRow )
                    // it starts indexing from 0, that is why it is named 'maxRow'.     ( i < maxRow )
                    int maxRow = table.Rows.Count;
                    for (int i = FIELDS_ROW_INDEX + 1; i < maxRow; i++)
                    {
                        var recordRow = table.Rows[i];

                        TDto dto = _mapper.MapFromDataRow(recordRow);
                        dtos.Add(dto);
                    }

                    return dtos;
                }
            }
        }

        public IList<TDto> SaveToDb(IList<TDto> dtos)
        {
            IList<TEntity> entities = _mapper.Map(dtos);

            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();

            return _mapper.Map(entities);
        }
    }
}
