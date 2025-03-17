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
    }
}
