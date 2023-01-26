using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WorkOrders_BAL.Entities.Base;

namespace WorkOrders_BAL.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class LeadtechEntity : EntityBase
    {
        public LeadtechEntity() : base() { }
        public LeadtechEntity(Guid id) : base(id) { }

        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
