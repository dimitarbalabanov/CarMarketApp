namespace CarMarket.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CarMarket.Common;
    using CarMarket.Data.Models;
    using CarMarket.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.userManager.Users
                .Include(u => u.Listings)
                .ToListAsync();
            return this.View(users);
        }
    }
}
