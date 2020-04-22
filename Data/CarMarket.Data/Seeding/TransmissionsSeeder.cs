namespace CarMarket.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarMarket.Data.Models;

    internal class TransmissionsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Transmissions.Any())
            {
                return;
            }

            var transmissions = new Transmission[]
            {
                new Transmission { Type = "Other" },
                new Transmission { Type = "Automatic" },
                new Transmission { Type = "Manual" },
            };

            await dbContext.Transmissions.AddRangeAsync(transmissions);
        }
    }
}
