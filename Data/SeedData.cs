using Microsoft.EntityFrameworkCore;
using WorkshopRSVP.Models;

namespace WorkshopRSVP.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Creates the database if needed and applies pending migrations
            context.Database.Migrate();

            // Seed only once
            if (context.Rsvps.Any())
            {
                return;
            }

            context.Rsvps.AddRange(
                new RSVP { FullName = "Alice Johnson", WorkshopName = "Routing Basics", NeedsAccommodation = false },
                new RSVP { FullName = "Bob Smith", WorkshopName = "EF Core Intro", NeedsAccommodation = true },
                new RSVP { FullName = "Carla Gomez", WorkshopName = "Razor Essentials", NeedsAccommodation = false },
                new RSVP { FullName = "David Lee", WorkshopName = "Routing Basics", NeedsAccommodation = false },
                new RSVP { FullName = "Emma Wilson", WorkshopName = "EF Core Intro", NeedsAccommodation = true }
            );

            context.SaveChanges();
        }
    }
}