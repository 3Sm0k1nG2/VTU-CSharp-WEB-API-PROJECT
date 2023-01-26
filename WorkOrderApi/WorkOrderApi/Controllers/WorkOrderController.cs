using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkOrders_BAL.Dtos.WorkOrder;
using WorkOrders_BAL.Entities.Auth;
using WorkOrders_DAL;
using WorkOrders_DAL.Interfaces;
using WorkOrders_DAL.Mappers.Excel;
using WorkOrders_DAL.Mappers.Repositories;
using WorkOrders_DAL.Repositories;
using WorkOrders_DAL.Services;

namespace WorkOrderApi.Controllers
{
    [Authorize(Roles = UserRoles.Employee)]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrderController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        protected readonly WorkOrderRepository _repository;

        public WorkOrderController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._repository = unitOfWork.WorkOrder;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public virtual async Task<ActionResult<WorkOrderDto>> Post(WorkOrderDto dto)
        {
            return Ok(await _repository.Add(dto));
        }

        [HttpGet]
        public virtual async Task<ActionResult<IList<WorkOrderDto>>> GetAll(
            [FromQuery] uint page = 0,
            [FromQuery] string? district = null,
            [FromQuery] string? leadtech = null,
            [FromQuery] string? service = null,
            [FromQuery] string? payment = null
        )
        {
            return Ok(await _repository.GetAll(page));
        }

        [HttpGet("rush")]
        public virtual ActionResult<WorkOrderDto> GetRushJobs()
        {
            return Ok(_repository.GetRushJobs());
        }

        [HttpGet("rush/count")]
        public virtual int GetRushJobsCount()
        {
            return _repository.GetRushJobsCount();
        }

        [HttpGet("cost/sum")]
        public virtual decimal GetTotalCost()
        {
            return _repository.GetTotalCost();
        }

        [HttpGet("cost/avg")]
        public virtual decimal GetAverageCost()
        {
            return _repository.GetAverageCost();
        }

        [HttpGet("most/{s}")]
        public virtual string GetMostFound(
            string s
        )
        {
            return _repository.GetMostFound(s);
        }

        [HttpGet("{id:Guid}")]
        public virtual async Task<ActionResult<WorkOrderDto>> GetById(Guid id)
        {
            return Ok(await _repository.GetById(id));
        }

        [HttpGet("{id:Guid}/detailed")]
        public async virtual Task<ActionResult<WorkOrderDto>> GetByIdDetailed(Guid id)
        {
            return Ok(await _repository.GetByIdDetailed(id));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPatch("{id:Guid}")]
        public virtual async Task<ActionResult<WorkOrderDto>> Update(Guid id, WorkOrderPatchDto dto)
        {
            return Ok(await _repository.UpdateById(id, dto));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id:Guid}")]
        public virtual async Task<ActionResult<WorkOrderDto>> Delete(Guid id)
        {
            return Ok(await _repository.RemoveById(id));
        }

        // Below should be extracted elsewhere
        // _unitOfWork should be extracted too

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("excel")]
        public async Task<ActionResult<WorkOrderDto>> getExcel()
        {
            WorkOrderExcelService excelService = GetExcelService();

            IList<WorkOrderDto> dtos = excelService.Read(Constants.EXCEL_FILE_PATH);

            dtos = await excelService.SaveToDb(dtos);

            return Ok(dtos);
        }

        private WorkOrderExcelService GetExcelService()
        {
            return new WorkOrderExcelService(
                _unitOfWork.Context,
                new WorkOrderExcelMapper(),
                new WorkOrderMapper(new WorkOrderRequiredRepositories(_unitOfWork))
            );
        }


    }
}
