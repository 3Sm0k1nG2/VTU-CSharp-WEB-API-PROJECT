using ExcelDataReader;
using WorkOrders_BAL.Dtos.WorkOrder;
using WorkOrders_BAL.Entities;
using WorkOrders_DAL.DbContexts;
using WorkOrders_DAL.Interfaces.Mappers.Entity.Base;
using WorkOrders_DAL.Interfaces.Mappers.Excel.Base;
using WorkOrders_DAL.Mappers.Excel;
using WorkOrders_DAL.Mappers.Repositories;

namespace WorkOrders_DAL.Services
{
    public class WorkOrderExcelService
    {
        private WorkOrderDbContext _context;
        private WorkOrderExcelMapper _excelMapper;
        private WorkOrderMapper _mapper;

        public WorkOrderExcelService(WorkOrderDbContext context, WorkOrderExcelMapper excelMapper, WorkOrderMapper mapper)
        {
            _context = context;
            _excelMapper = excelMapper;
            _mapper = mapper;
        }

        public IList<WorkOrderDto> Read(string filePath)
        {
            IList<WorkOrderDto> dtos = new List<WorkOrderDto>();

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

                        WorkOrderDtoExcel dto = _excelMapper.MapFromDataRow(recordRow);
                        dtos.Add(_mapper.Map(dto));
                    }

                    return dtos;
                }
            }
        }

        public async Task<IList<WorkOrderDto>> SaveToDb(IList<WorkOrderDto> dtos)
        {
            IList<WorkOrderEntity> entities = _mapper.Map(dtos);

            await _context.Set<WorkOrderEntity>().AddRangeAsync(entities);
            _context.SaveChanges();

            return _mapper.Map(entities);
        }
    }
}