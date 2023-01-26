using WorkOrders_BAL.Dtos.Base;

namespace WorkOrders_BAL.Dtos
{
    public class LaborRateDto : DtoBase
    {
        public int TechsCount { get; set; }
        public decimal LbrRate { get; set; }
    }
}
