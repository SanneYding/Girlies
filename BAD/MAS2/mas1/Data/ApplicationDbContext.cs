using Microsoft.EntityFrameworkCore;
using mas1.Models; 

namespace mas1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<SharedExperience> SharedExperiences { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<ExperienceBooking> ExperienceBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            Seed(modelBuilder); 
        }

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Experience>().HasData(
                new Experience { Id = 1, Name = "Skydiving" },
                new Experience { Id = 2, Name = "Scuba Diving" },
                new Experience { Id = 3, Name = "Mountain Hiking" }
            );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { Id = 1, GuestName = "John Doe" },
                new Reservation { Id = 2, GuestName = "Jane Smith" },
                new Reservation { Id = 3, GuestName = "Alice Johnson" }
            );

            modelBuilder.Entity<Discount>().HasData(
                new Discount { Id = 1, Code = "SUMMER2025", Amount = 10 },
                new Discount { Id = 2, Code = "WINTER2025", Amount = 15 }
            );

            modelBuilder.Entity<SharedExperience>().HasData(
                new SharedExperience { Id = 1, ExperienceId = 1, DiscountId = 1 },
                new SharedExperience { Id = 2, ExperienceId = 2, DiscountId = 2 }
            );

            modelBuilder.Entity<ExperienceBooking>().HasData(
                new ExperienceBooking { Id = 1, ExperienceId = 1, ReservationId = 1 },
                new ExperienceBooking { Id = 2, ExperienceId = 2, ReservationId = 2 },
                new ExperienceBooking { Id = 3, ExperienceId = 3, ReservationId = 3 }
            );
        }
    }
}
