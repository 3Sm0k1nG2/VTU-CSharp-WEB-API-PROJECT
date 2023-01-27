using WorkOrders_BAL.Dtos;
using WorkOrders_BAL.Dtos.WorkOrder;
using WorkOrders_BAL.Entities;
using WorkOrders_DAL.Errors;
using WorkOrders_DAL.Interfaces.Mappers.Entity;
using WorkOrders_DAL.Mappers.Entity.Base;

namespace WorkOrders_DAL.Mappers.Repositories
{
    public sealed class WorkOrderMapper : EntityMapper<WorkOrderEntity, WorkOrderDto, WorkOrderPatchDto>, IWorkOrderMapper
    {
        private WorkOrderRequiredRepositories _requiredRepositories;
        private bool hasRequiredRepositories = false;

        private List<DistrictDto> Districts { get; set; }
        private List<LeadtechDto> Leadtechs { get; set; }
        private List<ServiceDto> Services { get; set; }
        private List<PaymentDto> Payments { get; set; }
        private List<LaborRateDto> LaborRates { get; set; }
        private List<WeekdayDto> Weekdays { get; set; }

        public WorkOrderMapper(WorkOrderRequiredRepositories requiredRepositories)
        {
            _requiredRepositories = requiredRepositories;
        }

        private void initalizeRequiredRepositories()
        {
            if (!hasRequiredRepositories)
            {
                Districts = _requiredRepositories.District.GetAll().Result.ToList();
                Leadtechs = _requiredRepositories.Leadtech.GetAll().Result.ToList();
                Services = _requiredRepositories.Service.GetAll().Result.ToList();
                Payments = _requiredRepositories.Payment.GetAll().Result.ToList();
                LaborRates = _requiredRepositories.LaborRate.GetAll().Result.ToList();
                Weekdays = _requiredRepositories.Weekday.GetAll().Result.ToList();

                hasRequiredRepositories = true;
            }
        }

        public new void Patch(WorkOrderEntity entity, WorkOrderPatchDto dto)
        {
            initalizeRequiredRepositories();

            // WorkOrder Id
            if (dto.WO != null)
            {
                entity.WO = dto.WO;
            }

            // Relations
            if (dto.District != null)
            {
                entity.DistrictId = Districts.First(x => x.Name == dto.District).Id;
            }
            if (dto.Leadtech != null)
            {
                entity.LeadtechId = Leadtechs.First(x => x.Name == dto.Leadtech).Id;
            }
            if (dto.Service != null)
            {
                entity.ServiceId = Services.First(x => x.Name == dto.Service).Id;
            }
            if (dto.Payment != null)
            {
                entity.PaymentId = Payments.First(x => x.Name == dto.Payment).Id;
            }

            // Fields
            if (dto.Rush != null)
            {
                entity.Rush = (bool)dto.Rush;
            }
            if (dto.ReqDate != null)
            {
                entity.ReqDate = (DateTime)dto.ReqDate;
            }
            if (dto.WorkDate != null)
            {
                var workDate = (DateTime)dto.WorkDate;

                if(workDate != default(DateTime))
                {
                    entity.WorkDate = workDate;
                }
            }
            else
            {
                entity.WorkDate = dto.WorkDate;
            }
            if (dto.Techs != null)
            {
                if (!LaborRates.Exists(x => x.TechsCount == dto.Techs))
                {
                    throw new Exception(Constants.ERROR_TECHS_COUNT_NOT_MATCHING);
                }

                entity.Techs = (int)dto.Techs;
            }
            if (dto.WtyLbr != null)
            {
                entity.WtyLbr = (bool)dto.WtyLbr;
            }
            if (dto.WtyParts != null)
            {
                entity.WtyParts = (bool)dto.WtyParts;
            }
            if (dto.LbrHrs != null)
            {
                entity.LbrHrs = dto.LbrHrs;
            }
            if (dto.PartsCost != null)
            {
                entity.PartsCost = (decimal)dto.PartsCost;
            }
        }
        public override WorkOrderEntity Map(WorkOrderDto dto)
        {
            initalizeRequiredRepositories();

            if (!LaborRates.Exists(x => x.TechsCount == dto.Techs))
            {
                throw new Exception(Constants.ERROR_TECHS_COUNT_NOT_MATCHING);
            }

            WorkOrderEntity entity = new WorkOrderEntity(dto.Id)
            {
                // WorkOrder Id
                WO = dto.WO,

                // Relations
                DistrictId = Districts.First(x => x.Name == dto.District).Id,
                LeadtechId = Leadtechs.First(x => x.Name == dto.Leadtech).Id,
                ServiceId = Services.First(x => x.Name == dto.Service).Id,
                PaymentId = Payments.First(x => x.Name == dto.Payment).Id,

                // Fields
                Rush = dto.Rush,
                ReqDate = (DateTime)dto.ReqDate,
                WorkDate = dto.WorkDate,
                Techs = (int)dto.Techs,
                WtyLbr = dto.WtyLbr,
                WtyParts = dto.WtyParts,
                LbrHrs = dto.LbrHrs,
                PartsCost = (decimal)dto.PartsCost
            };

            return entity;
        }

        public WorkOrderDto Map(WorkOrderDtoExcel dtoExcel)
        {
            WorkOrderDto dto = new WorkOrderDto(new Guid())
            {
                // WorkOrder Id
                WO = dtoExcel.WO,

                // Relations
                District = dtoExcel.District,
                Leadtech = dtoExcel.Leadtech,
                Service = dtoExcel.Service,
                Payment = dtoExcel.Payment,

                // Fields
                Rush = dtoExcel.Rush,
                ReqDate = (DateTime)dtoExcel.ReqDate,
                WorkDate = dtoExcel.WorkDate,
                Techs = (int)dtoExcel.Techs,
                WtyLbr = dtoExcel.WtyLbr,
                WtyParts = dtoExcel.WtyParts,
                LbrHrs = dtoExcel.LbrHrs,
                PartsCost = (decimal)dtoExcel.PartsCost
            };

            return dto;
        }

        public override WorkOrderDto Map(WorkOrderEntity entity)
        {
            initalizeRequiredRepositories();

            WorkOrderDto dto = new WorkOrderDto(entity.Id)
            {
                // WorkOrder Id
                WO = entity.WO,

                // Relations
                District = Districts.First(x => x.Id == entity.DistrictId).Name,
                Leadtech = Leadtechs.First(x => x.Id == entity.LeadtechId).Name,
                Service = Services.First(x => x.Id == entity.ServiceId).Name,
                Payment = Payments.First(x => x.Id == entity.PaymentId).Name,

                // Fields
                Rush = entity.Rush,
                ReqDate = entity.ReqDate,
                WorkDate = entity.WorkDate,
                Techs = entity.Techs,
                WtyLbr = entity.WtyLbr,
                WtyParts = entity.WtyParts,
                LbrHrs = entity.LbrHrs,
                PartsCost = entity.PartsCost
            };

            //dto.SetInitId(entity.Id);

            return dto;
        }
        public override IList<WorkOrderEntity> Map(IList<WorkOrderDto> sList)
        {
            IList<WorkOrderEntity> fList = new List<WorkOrderEntity>();

            foreach (WorkOrderDto s in sList)
            {
                fList.Add(this.Map(s));
            }

            return fList;
        }
        public override IList<WorkOrderDto> Map(IList<WorkOrderEntity> fList)
        {
            IList<WorkOrderDto> sList = new List<WorkOrderDto>();

            foreach (WorkOrderEntity f in fList)
            {
                sList.Add(this.Map(f));
            }

            return sList;
        }

        public WorkOrderDetailedDto MapDetailed(WorkOrderDto dto)
        {
            initalizeRequiredRepositories();

            WorkOrderDetailedDto detailedDto = new WorkOrderDetailedDto(dto.Id)
            {
                WO = dto.WO,

                // Relations
                District = dto.District,
                Leadtech = dto.Leadtech,
                Service = dto.Service,
                Payment = dto.Payment,

                // Fields
                Rush = dto.Rush,
                ReqDate = dto.ReqDate,
                WorkDate = dto.WorkDate,
                Techs = dto.Techs,
                WtyLbr = dto.WtyLbr,
                WtyParts = dto.WtyParts,
                LbrHrs = dto.LbrHrs,
                PartsCost = dto.PartsCost
            };

            // Additional calculated fields
            detailedDto.LbrRate = LaborRates.First(x => x.TechsCount == detailedDto.Techs).LbrRate;
            detailedDto.LbrCost = detailedDto.LbrRate * detailedDto.LbrHrs;
            detailedDto.LbrFee = (bool)detailedDto.WtyLbr ? null : detailedDto.LbrCost; // labor cost, if not under warranty
            detailedDto.PartsFee = (bool)detailedDto.WtyParts ? null : detailedDto.PartsCost; // parts cost, if not under warranty
            detailedDto.TotalCost = (decimal)(detailedDto.LbrCost == null ? detailedDto.PartsCost : (decimal)detailedDto.LbrCost + detailedDto.PartsCost);
            detailedDto.TotalFee = (detailedDto.LbrFee == null && detailedDto.PartsFee == null) ? null : detailedDto.LbrFee + detailedDto.PartsFee;

            detailedDto.ReqDay = Weekdays.First(x =>
            {
                DateTime a = (DateTime)detailedDto.ReqDate;
                return x.DayOfWeek == (sbyte)a.DayOfWeek;
            }).Name;

            if (detailedDto.WorkDate == null)
            {
                detailedDto.Wait = null;
                detailedDto.WorkDay = null;
            }
            else
            {
                DateTime workDate = (DateTime)detailedDto.WorkDate;

                detailedDto.Wait = (int)(workDate - (DateTime)detailedDto.ReqDate).TotalDays;
                detailedDto.WorkDay = Weekdays.First(x => x.DayOfWeek == ((sbyte)workDate.DayOfWeek)).Name;
            }

            //dto.SetInitId(entity.Id);

            return detailedDto;
        }
        public WorkOrderDetailedDto MapDetailed(WorkOrderEntity entity)
        {
            return MapDetailed(Map(entity));
        }
        public IList<WorkOrderDetailedDto> MapDetailed(IList<WorkOrderDto> dtos)
        {
            IList<WorkOrderDetailedDto> detailedDtos = new List<WorkOrderDetailedDto>();

            foreach (WorkOrderDto dto in dtos)
            {
                detailedDtos.Add(MapDetailed(dto));
            }

            return detailedDtos;
        }
    }
}
