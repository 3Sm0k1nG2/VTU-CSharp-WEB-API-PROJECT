using WorkOrders_BAL.Interfaces;

namespace WorkOrders_DAL.Interfaces.Repositories
{
    public interface IRepository<TEntity, TDto, TPatchDto>
        where TEntity : class, IEntity, new()
        where TDto : class, IDto, new()
        where TPatchDto : IDto
    {
        #region Basic CRUD
        Task<TDto> Add(TDto dto);

        Task<IEnumerable<TDto>> GetAll();

        Task<TDto> GetById(Guid id);

        Task<TDto> UpdateById(Guid id, TPatchDto dto);

        Task<TDto> RemoveById(Guid id);
        #endregion
    }
}
