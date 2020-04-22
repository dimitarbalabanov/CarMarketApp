namespace CarMarket.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarMarket.Data.Models;

    internal class BodiesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Bodies.Any())
            {
                return;
            }

            var bodies = new Body[]
            {
                new Body { Type = "Convertible" },
                new Body { Type = "Coupe" },
                new Body { Type = "Hatchback" },
                new Body { Type = "Sedan" },
                new Body { Type = "Pickup" },
                new Body { Type = "SUV" },
                new Body { Type = "Van" },
                new Body { Type = "Wagon" },
            };

            await dbContext.Bodies.AddRangeAsync(bodies);
        }
    }
}
