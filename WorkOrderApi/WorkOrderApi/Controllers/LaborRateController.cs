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
    public class LaborRateController : ControllerBase
    {
        protected readonly LaborRateRepository _repository;

        public LaborRateController(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.LaborRate;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public virtual async Task<ActionResult<LaborRateDto>> Post(LaborRateDto dto)
        {
            return Ok(await _repository.Add(dto));
        }

        [HttpGet]
        public virtual async Task<ActionResult<IList<LaborRateDto>>> GetAll(
            [FromQuery] uint? techsMax,
            [FromQuery] uint? rateMax,
            [FromQuery] uint techsMin = 0,
            [FromQuery] uint rateMin = 0
        )
        {
            return Ok(_repository.GetAll(techsMax, rateMax, techsMin, rateMin));
        }

        [HttpGet("{id:Guid}")]
        public virtual async Task<ActionResult<LaborRateDto>> GetById(Guid id)
        {
            return Ok(await _repository.GetById(id));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPatch("{id:Guid}")]
        public virtual async Task<ActionResult<LaborRateDto>> Update(Guid id, LaborRateDto dto)
        {
            return Ok(await _repository.UpdateById(id, dto));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id:Guid}")]
        public virtual async Task<ActionResult<LaborRateDto>> Delete(Guid id)
        {
            return Ok(await _repository.RemoveById(id));
        }
    }
}
