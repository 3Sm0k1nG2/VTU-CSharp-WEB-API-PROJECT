using WorkOrders_BAL.Dtos.Base;

namespace WorkOrders_BAL.Dtos
{
    public class WeekdayDto : DtoBase
    {
        public string Name { get; set; }
        public int DayOfWeek { get; set; }
    }
}
