using Microsoft.EntityFrameworkCore;
using ExperienceAPI.Models;

namespace ExperienceAPI.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<SharedExperience> SharedExperiences { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ExperienceBooking> ExperienceBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SharedExperienceDiscount>()
                .HasKey(sd => new { sd.SharedExperienceId, sd.DiscountId });
            modelBuilder.Entity<ExperienceBookingDiscount>()
                .HasKey(ed => new { ed.ExperienceBookingId, ed.DiscountId });

            modelBuilder.Entity<SharedExperienceDiscount>()
                .HasOne(sd => sd.SharedExperience)
                .WithMany(se => se.Discounts)
                .HasForeignKey(sd => sd.SharedExperienceId);

            modelBuilder.Entity<SharedExperienceDiscount>()
                .HasOne(sd => sd.Discount)
                .WithMany(d => d.SharedExperienceDiscounts)
                .HasForeignKey(sd => sd.DiscountId);

            modelBuilder.Entity<ExperienceBookingDiscount>()
                .HasOne(ed => ed.ExperienceBooking)
                .WithMany(eb => eb.ExperienceBookingDiscounts)
                .HasForeignKey(ed => ed.ExperienceBookingId);

            modelBuilder.Entity<ExperienceBookingDiscount>()
                .HasOne(ed => ed.Discount)
                .WithMany(d => d.ExperienceBookingDiscounts)
                .HasForeignKey(ed => ed.DiscountId);

            
        }
    }
}