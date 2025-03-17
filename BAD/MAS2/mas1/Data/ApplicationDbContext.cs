using Microsoft.EntityFrameworkCore;
using mas1.Models;

namespace mas1.Data
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

            public DbSet<Guest> Guest { get; set; }
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
                // Seed Providers first, since Experiences will reference them.
                modelBuilder.Entity<Provider>().HasData(
                    new Provider { ProviderID = 1, BusinessAddress = "Finlands Gade 27", CVR = "98765432", PhoneNumber = "98765432" },
                    new Provider { ProviderID = 2, BusinessAddress = "Finlands Gade 25", CVR = "564323567", PhoneNumber = "35246543" },
                    new Provider { ProviderID = 3, BusinessAddress = "Finlands Gade 22", CVR = "42356754", PhoneNumber = "5465436" }
                );

                // Seed Experiences after Providers
                modelBuilder.Entity<Experience>().HasData(
                    new Experience { ExperienceID = 1, Name = "Skydiving", Description = "Jumping out of a plane", Price = 250, ProviderID = 1 },
                    new Experience { ExperienceID = 2, Name = "Scuba Diving", Description = "Swimming with sharks", Price = 200, ProviderID = 2 },
                    new Experience { ExperienceID = 3, Name = "Mountain Hiking", Description = "Hiking above the clouds", Price = 100, ProviderID = 3 }
                );

                // Seed Discounts
                modelBuilder.Entity<Discount>().HasData(
                    new Discount { DiscountID = 1, GroupSize = 10, DiscountPercentage = 10 },
                    new Discount { DiscountID = 2, GroupSize = 15, DiscountPercentage = 15 }
                );

                // Seed SharedExperiences after Discounts and Experiences
                modelBuilder.Entity<SharedExperience>().HasData(
                    new SharedExperience { SharedExperienceID = 1, ExperienceId = 1, DiscountId = 1, Date = new DateTime(2024, 03, 17, 12, 0, 0), ExperienceName = "Test 1" },
                    new SharedExperience { SharedExperienceID = 2, ExperienceId = 2, DiscountId = 2, Date = new DateTime(2024, 03, 17, 12, 0, 0), ExperienceName = "Test 2" }
                );

                // Seed Guests after the SharedExperience table
                modelBuilder.Entity<Guest>().HasData(
                    new Guest { GuestID = 22, Name = "John Watson", Age = 54, PhoneNumber = "8888 8888" },
                    new Guest { GuestID = 23, Name = "Sherlock Holmes", Age = 34, PhoneNumber = "2222 2222" },
                    new Guest { GuestID = 24, Name = "Lara Croft", Age = 25, PhoneNumber = "1234 5678" }
                );

                // Seed Reservations after Guests and SharedExperiences
                modelBuilder.Entity<Reservation>().HasData(
                    new Reservation { ReservationID = 1, GuestID = 22, SharedExperienceID = 1 },
                    new Reservation { ReservationID = 2, GuestID = 23, SharedExperienceID = 2 },
                    new Reservation { ReservationID = 3, GuestID = 24, SharedExperienceID = 1 }
                );

                // Seed ExperienceBookings after Reservations
                modelBuilder.Entity<ExperienceBooking>().HasData(
                    new ExperienceBooking { ExperienceBookingID = 1, ExperienceID = 1, ReservationID = 1, GuestID = 22 },
                    new ExperienceBooking { ExperienceBookingID = 2, ExperienceID = 2, ReservationID = 2, GuestID = 23},
                    new ExperienceBooking { ExperienceBookingID = 3, ExperienceID = 3, ReservationID = 3, GuestID = 24 }
                );
            }

        }
    }

