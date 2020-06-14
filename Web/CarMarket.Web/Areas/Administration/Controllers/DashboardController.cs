namespace CarMarket.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CarMarket.Common;
    using CarMarket.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class DashboardController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return this.View();
        }
    }
}
