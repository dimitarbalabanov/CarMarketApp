namespace CarMarket.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarMarket.Data.Models;

    internal class FuelsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Fuels.Any())
            {
                return;
            }

            var fuels = new Fuel[]
            {
                new Fuel { Type = "Other" },
                new Fuel { Type = "Petrol" },
                new Fuel { Type = "Diesel" },
                new Fuel { Type = "LPG" },
            };

            await dbContext.Fuels.AddRangeAsync(fuels);
        }
    }
}
