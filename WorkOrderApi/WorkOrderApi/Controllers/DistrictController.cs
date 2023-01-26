using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkOrders_BAL.Dtos;
using WorkOrders_BAL.Entities.Auth;
using WorkOrders_DAL.Interfaces;
using WorkOrders_DAL.Repositories;

namespace WorkOrderApi.Controllers
{
    [Authorize(Roles = UserRoles.Employee)]
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        protected readonly DistrictRepository _repository;

        public DistrictController(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.District;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public virtual async Task<ActionResult<DistrictDto>> Post(DistrictDto dto)
        {
            return Ok(await _repository.Add(dto));
        }

        [HttpGet]
        public virtual async Task<ActionResult<IList<DistrictDto>>> GetAll(
            [FromQuery] string? col,
            [FromQuery] string? name
        )
        {
            return Ok(_repository.GetAll(col, name));
        }

        [Authorize(Roles = UserRoles.Employee)]
        [HttpGet("{id:Guid}")]
        public virtual async Task<ActionResult<DistrictDto>> GetById(Guid id)
        {
            return Ok(await _repository.GetById(id));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPatch("{id:Guid}")]
        public virtual async Task<ActionResult<DistrictDto>> Update(Guid id, DistrictDto dto)
        {
            return Ok(await _repository.UpdateById(id, dto));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id:Guid}")]
        public virtual async Task<ActionResult<DistrictDto>> Delete(Guid id)
        {
            return Ok(await _repository.RemoveById(id));
        }
    }
}
