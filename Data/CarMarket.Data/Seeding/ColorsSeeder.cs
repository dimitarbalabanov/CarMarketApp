namespace CarMarket.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarMarket.Data.Models;

    internal class ColorsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Colors.Any())
            {
                return;
            }

            var colors = new Color[]
            {
                new Color { Name = "White" },
                new Color { Name = "Silver" },
                new Color { Name = "Black" },
                new Color { Name = "Grey" },
                new Color { Name = "Blue" },
                new Color { Name = "Red" },
                new Color { Name = "Brown" },
                new Color { Name = "Green" },
            };

            await dbContext.Colors.AddRangeAsync(colors);
        }
    }
}
