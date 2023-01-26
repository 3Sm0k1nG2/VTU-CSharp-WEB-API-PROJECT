namespace WorkOrders_BAL.Dtos.WorkOrder
{
    public class WorkOrderDetailedDto : WorkOrderDto
    {
        public WorkOrderDetailedDto() : base() { }
        public WorkOrderDetailedDto(Guid id) : base(id) { }


        // Additional calculated fields
        public int? Wait { get; set; }
        public decimal LbrRate { get; set; }                       // hourly rate lookup, based on number of technicians
        public decimal? LbrCost { get; set; }                   // LbrRate x LbrHrs
        public decimal? LbrFee { get; set; }                    // labor cost, if not under warranty
        public decimal? PartsFee { get; set; }                  // parts cost, if not under warranty
        public decimal TotalCost { get; set; }                 // LbrCost + PartsCost
        public decimal? TotalFee { get; set; }                  // LbrFee + PartsFee
        public string ReqDay { get; set; }                     // 3-letter weekday name, based on ReqDate
        public string? WorkDay { get; set; }                    // 3-letter weekday name, based on WorkDate
    }
}
