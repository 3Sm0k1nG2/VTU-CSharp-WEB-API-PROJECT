using WorkOrders_BAL.Interfaces;

namespace WorkOrders_DAL.Interfaces.Mappers.Entity.Base
{
    public interface IEntityMapper<TEntity, TDto, TDtoPatch> : IMapper<TEntity, TDto>
        where TEntity : IEntity
        where TDto : IDto
        where TDtoPatch : IDto
    {
        void Patch(TEntity entity, TDtoPatch dto);
    }
}
