using WorkOrders.Interfaces;

namespace Project
{
    public class WorkOrderModel
    {
        public int Id { get; set; } // unique work order ID
        public int DistrictId { get; set; }      // unique district ID
        public int LeadTechId { get; set; }      // unique lead_tech ID
        public int ServiceId { get; set; }       // unique service ID
        public bool Rush { get; set; }           // is this a rush job? Yes or blank
        public DateTime ReqDate { get; set; }    // date the work order was entered in system
        public DateTime WokDate { get; set; }    // date the work was completed
        public int Techs { get; set; }           // number of technicians required
        public bool WtyLbr { get; set; }         // is labor under warranty? Yes or blank
        public bool WtyParts { get; set; }       // are parts under warranty? Yes or blank
        public int LbrHrs { get; set; }          // for completed work orders - number of hours labour
        public decimal PartsCost { get; set; }   // cost of parts required to complete the work
        public int PaymentId { get; set; }       // type of payment customer will use - Account, C.O.D, Credit, P.O., Warranty

        // Additional calculated fields
        public int Wait { get; set; }           // number of days waited -> WorkDate - ReqDate
        public int LbrRate { get; set; }        // hourly rate lookup, based on number of technicians
        public decimal LbrCost { get; set; }    // LbrRate x LbrHrs
        public decimal LbrFee { get; set; }     // labor cost, if not under warranty
        public decimal PartsFee { get; set; }   // parts cost, if not under warranty
        public decimal TotalCost { get; set; }  // LbrCost + PartsCost
        public decimal TotalFee { get; set; }   // LbrFee + PartsFee
        public int ReqDay_ID { get; set; }      // 3-letter weekday name, based on ReqDate
        public int WorkDay_ID { get; set; }     // 3-letter weekday name, based on WorkDate
    }
}