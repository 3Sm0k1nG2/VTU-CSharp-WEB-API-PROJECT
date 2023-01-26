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
    public class PaymentController : ControllerBase
    {
        protected readonly PaymentRepository _repository;

        public PaymentController(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.Payment;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public virtual async Task<ActionResult<PaymentDto>> Post(PaymentDto dto)
        {
            return Ok(await _repository.Add(dto));
        }

        [HttpGet]
        public virtual async Task<ActionResult<IList<PaymentDto>>> GetAll(
            [FromQuery] string? col,
            [FromQuery] string? name
        )
        {
            return Ok(_repository.GetAll(col, name));
        }

        [HttpGet("{id:Guid}")]
        public virtual async Task<ActionResult<PaymentDto>> GetById(Guid id)
        {
            return Ok(await _repository.GetById(id));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPatch("{id:Guid}")]
        public virtual async Task<ActionResult<PaymentDto>> Update(Guid id, PaymentDto dto)
        {
            return Ok(await _repository.UpdateById(id, dto));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id:Guid}")]
        public virtual async Task<ActionResult<PaymentDto>> Delete(Guid id)
        {
            return Ok(await _repository.RemoveById(id));
        }
    }
}
