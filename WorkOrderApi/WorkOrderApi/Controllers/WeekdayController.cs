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
    public class WeekdayController: ControllerBase
    {
        protected readonly WeekdayRepository _repository;

        public WeekdayController(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.Weekday;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public virtual async Task<ActionResult<WeekdayDto>> Post(WeekdayDto dto)
        {
            return Ok(await _repository.Add(dto));
        }

        [HttpGet]
        public virtual async Task<ActionResult<IList<WeekdayDto>>> GetAll(
            [FromQuery] string? col,
            [FromQuery] string? day
        )
        {
            return Ok(_repository.GetAll(col, day));
        }

        [HttpGet("{id:Guid}")]
        public virtual async Task<ActionResult<WeekdayDto>> GetById(Guid id)
        {
            return Ok(await _repository.GetById(id));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPatch("{id:Guid}")]
        public virtual async Task<ActionResult<WeekdayDto>> Update(Guid id, WeekdayDto dto)
        {
            return Ok(await _repository.UpdateById(id, dto));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id:Guid}")]
        public virtual async Task<ActionResult<WeekdayDto>> Delete(Guid id)
        {
            return Ok(await _repository.RemoveById(id));
        }
    }
}
