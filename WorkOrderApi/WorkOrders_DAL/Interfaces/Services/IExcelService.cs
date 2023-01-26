using WorkOrders_BAL.Interfaces;

namespace WorkOrders_DAL.Interfaces.Services
{
    public interface IExcelService<TDto>
        where TDto : IDto
    {
        IList<TDto> Read(string filePath);
        IList<TDto> SaveToDb(IList<TDto> dtos);
    }
}
