using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WorkOrders_BAL.Entities.Base;

namespace WorkOrders_BAL.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class WeekdayEntity : EntityBase
    {
        public WeekdayEntity() { }

        public WeekdayEntity(Guid id) : base(id) { }

        [Required]
        [MaxLength(7)]
        public int DayOfWeek { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
