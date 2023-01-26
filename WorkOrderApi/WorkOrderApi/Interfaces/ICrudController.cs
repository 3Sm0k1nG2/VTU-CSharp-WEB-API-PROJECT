using Microsoft.AspNetCore.Mvc;
using WorkOrders_BAL.Interfaces;

namespace WorkOrderApi.Interfaces
{
    public interface ICrudController<TEntity, TDto, TDtoPatch>
        where TEntity : IEntity
        where TDto : IDto
        where TDtoPatch : IDto
    {
        [HttpPost]
        Task<ActionResult<TDto>> Post(TDto dto);

        [HttpGet]
        Task<ActionResult<IList<TDto>>> GetAll();

        [HttpGet("{id:Guid}")]
        Task<ActionResult<TDto>> GetById(Guid id);

        [HttpPut("{id:Guid}")]
        Task<ActionResult<TDto>> Update(Guid id, TDtoPatch dto);

        [HttpDelete("{id:Guid}")]
        Task<ActionResult<TDto>> Delete(Guid id);
    }
}
