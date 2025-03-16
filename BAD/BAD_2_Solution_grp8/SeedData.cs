using ExperienceAPI.Models;

namespace ExperienceAPI.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider, ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Providers.Any())
            {
                context.Providers.AddRange(
                    new Provider { Name = "Adventure Co." },
                    new Provider { Name = "Extreme Experiences" }
                );

                context.SaveChanges();
            }
        }
    }
}
