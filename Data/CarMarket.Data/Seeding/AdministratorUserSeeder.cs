namespace CarMarket.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarMarket.Common;
    using CarMarket.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class AdministratorUserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedAdminAsync(userManager, GlobalConstants.AdministratorRoleName);
        }

        private static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, string administratorRoleName)
        {
            var user = await userManager.FindByNameAsync(GlobalConstants.AdministratorUsername);
            if (user != null && (await userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName)))
            {
                return;
            }

            var admin = new ApplicationUser
            {
                UserName = GlobalConstants.AdministratorUsername,
                Email = GlobalConstants.AdministratorEmail,
                FirstName = GlobalConstants.AdministratorUsername,
                LastName = GlobalConstants.AdministratorUsername,
            };

            var result = await userManager.CreateAsync(admin, GlobalConstants.AdministratorPassword);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            }

            var roleResult = await userManager.AddToRoleAsync(admin, administratorRoleName);
            if (!roleResult.Succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            }
        }
    }
}
