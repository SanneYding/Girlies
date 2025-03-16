using Microsoft.EntityFrameworkCore;
using ExperienceAPI.Models;

namespace ExperienceAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<SharedExperience> SharedExperiences { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<ExperienceBooking> ExperienceBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SharedExperience>()
                .HasMany(s => s.Discounts)
                .WithMany(d => d.SharedExperiences);

            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.SharedExperiences)
                .WithMany(s => s.Reservations);

            modelBuilder.Entity<ExperienceBooking>()
                .HasMany(e => e.Discounts)
                .WithMany(d => d.ExperienceBookings);
        }
    }
}
