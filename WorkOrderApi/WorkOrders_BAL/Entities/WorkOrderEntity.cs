using Microsoft.EntityFrameworkCore;
using WorkOrders_BAL.Entities.Base;

namespace WorkOrders_BAL.Entities
{
    [Index(nameof(WO), IsUnique = true)]
    public class WorkOrderEntity : EntityBase
    {
        #region Constructors
        public WorkOrderEntity() : base() { }
        public WorkOrderEntity(Guid id) : base(id) { }
        #endregion

        #region Fields
        public string WO { get; set; }              // unique work order ID - { A00000, A00001, ..., A99999, B00000, ..., F99999, ..., Z99999 }

        public Guid DistrictId { get; set; }        // unique district ID
        protected virtual DistrictEntity District { get; set; }

        public Guid LeadtechId { get; set; }        // unique lead_tech ID
        protected virtual LeadtechEntity Leadtech { get; set; }

        public Guid ServiceId { get; set; }         // unique service ID
        protected virtual ServiceEntity Service { get; set; }

        public bool Rush { get; set; }             // is this a rush job? Yes or blank
        public DateTime ReqDate { get; set; }       // date the work order was entered in system
        public DateTime? WorkDate { get; set; }     // date the work was completed
        public int Techs { get; set; }             // number of technicians required
        public bool WtyLbr { get; set; }           // is labor under warranty? Yes or blank
        public bool WtyParts { get; set; }         // are parts under warranty? Yes or blank
        public decimal? LbrHrs { get; set; }            // for completed work orders - number of hours labour
        public decimal PartsCost { get; set; }      // cost of parts required to complete the work

        public Guid PaymentId { get; set; }         // type of payment customer will use - Account, C.O.D, Credit, P.O., Warranty
        protected virtual PaymentEntity Payment { get; set; }
        #endregion
    }
}
