using Microsoft.EntityFrameworkCore;
using WorkOrders_BAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WorkOrders_DAL.DbContexts
{
    public class WorkOrderDbContext : IdentityDbContext<IdentityUser>/*, DbContext*/
    {
        public WorkOrderDbContext(DbContextOptions options) : base(options)
        {
            //Context = new DbContext(options);
        }

        //public DbContext Context { get; private set; }
        public DbSet<WorkOrderEntity> WorkOrders { get; set; }
        public DbSet<DistrictEntity> Districts { get; set; }
        public DbSet<LeadtechEntity> Leadtechs { get; set; }
        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<PaymentEntity> Payments { get; set; }
        public DbSet<WeekdayEntity> Weekdays { get; set; }
        public DbSet<LaborRateEntity> LaborRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<LeadtechEntity>()
           // .HasData(
           //     new LeadtechEntity { Name = "Burton" },
           //     new LeadtechEntity { Name = "Cartier" },
           //     new LeadtechEntity { Name = "Khan" },
           //     new LeadtechEntity { Name = "Ling" },
           //     new LeadtechEntity { Name = "Lopez" },
           //     new LeadtechEntity { Name = "Michner" }
           // );
           // modelBuilder.Entity<DistrictEntity>()
           // .HasData(
           //     new DistrictEntity { Name = "East" },
           //     new DistrictEntity { Name = "West" },
           //     new DistrictEntity { Name = "North" },
           //     new DistrictEntity { Name = "South" },
           //     new DistrictEntity { Name = "Central" },
           //     new DistrictEntity { Name = "Northeast" },
           //     new DistrictEntity { Name = "Northwest" },
           //     new DistrictEntity { Name = "Southeast" },
           //     new DistrictEntity { Name = "Southwest" }
           // );
           // modelBuilder.Entity<ServiceEntity>()
           // .HasData(
           //     new ServiceEntity { Name = "Assess" },
           //     new ServiceEntity { Name = "Deliver" },
           //     new ServiceEntity { Name = "Install" },
           //     new ServiceEntity { Name = "Repair" },
           //     new ServiceEntity { Name = "Replace" }
           // );
           // modelBuilder.Entity<PaymentEntity>()
           // .HasData(
           //     new PaymentEntity { Name = "Account" },
           //     new PaymentEntity { Name = "C.O.D." },
           //     new PaymentEntity { Name = "Credit" },
           //     new PaymentEntity { Name = "P.O." },
           //     new PaymentEntity { Name = "Warranty" }
           // );
           // modelBuilder.Entity<WeekdayEntity>()
           //.HasData(
           //      new WeekdayEntity { DayOfWeek = 1, Name = "Mon" },
           //      new WeekdayEntity { DayOfWeek = 2, Name = "Tue" },
           //      new WeekdayEntity { DayOfWeek = 3, Name = "Wed" },
           //      new WeekdayEntity { DayOfWeek = 4, Name = "Thu" },
           //      new WeekdayEntity { DayOfWeek = 5, Name = "Fri" },
           //      new WeekdayEntity { DayOfWeek = 6, Name = "Sat" },
           //      new WeekdayEntity { DayOfWeek = 7, Name = "Sun" }
           //  );
           // modelBuilder.Entity<LaborRateEntity>()
           // .HasData(
           //      new LaborRateEntity { TechsCount = 1, LbrRate = 80 },
           //      new LaborRateEntity { TechsCount = 2, LbrRate = 140 },
           //      new LaborRateEntity { TechsCount = 3, LbrRate = 195 }
           //  );

            base.OnModelCreating(modelBuilder);
        }
    }
}
