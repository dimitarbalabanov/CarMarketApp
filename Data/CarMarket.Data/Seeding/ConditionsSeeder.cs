namespace CarMarket.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarMarket.Data.Models;

    internal class ConditionsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Conditions.Any())
            {
                return;
            }

            var conditions = new Condition[]
            {
                new Condition { Type = "New" },
                new Condition { Type = "Used" },
            };

            await dbContext.Conditions.AddRangeAsync(conditions);
        }
    }
}
