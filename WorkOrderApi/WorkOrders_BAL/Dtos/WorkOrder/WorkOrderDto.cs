using WorkOrders_BAL.Dtos.Base;

namespace WorkOrders_BAL.Dtos.WorkOrder
{
    public class WorkOrderDto : DtoBase
    {
        // Constructors
        public WorkOrderDto() : base() { }
        public WorkOrderDto(Guid id) : base(id) { }
        

        // Fields
        public string WO { get; set; }        // unique work order ID
        public string District { get; set; }                 // geographic area where work will be done
        public string Leadtech { get; set; }               // head technician at the work location
        public string Service { get; set; }                 // type of work to be done - Assess, Deliver, Install, Repair, Replace
        public bool Rush { get; set; } = false;                 // is this a rush job? Yes or blank
        public DateTime ReqDate { get; set; } // date the work order was entered in system
        public DateTime? WorkDate { get; set; } = default(DateTime);                 // date the work was completed
        public int Techs { get; set; }                   // number of technicians required
        public bool WtyLbr { get; set; } = false;               // is labor under warranty? Yes or blank
        public bool WtyParts { get; set; } = false;             // are parts under warranty? Yes or blank
        public decimal? LbrHrs { get; set; }                     // for completed work orders - number of hours labour
        public decimal PartsCost { get; set; }              // cost of parts required to complete the work
        public string Payment { get; set; }    // type of payment customer will use - Account, C.O.D, Credit, P.O., Warranty
    }
}
