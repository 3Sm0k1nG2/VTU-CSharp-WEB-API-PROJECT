using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WorkOrders_BAL.Entities.Base;

namespace WorkOrders_BAL.Entities
{
    [Index(nameof(TechsCount), IsUnique = true)]
    public class LaborRateEntity : EntityBase
    {
        public LaborRateEntity() { }

        public LaborRateEntity(Guid id) : base(id) { }

        [Required]
        [MinLength(1)]
        public int TechsCount { get; set; }

        [Required]
        [MinLength(1)]
        public decimal LbrRate { get; set; }
    }
}
