namespace CarMarket.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CarMarket.Common;
    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class DashboardController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IListingsService listingsService;

        public DashboardController(IUsersService usersService, IListingsService listingsService)
        {
            this.usersService = usersService;
            this.listingsService = listingsService;
        }

        public async Task<IActionResult> Index()
        {
            var totalUsers = await this.usersService.GetTotalCountAsync();
            var totalListings = await this.listingsService.GetTotalCountAsync();

            var viewModel = new DashboardViewModel
            {
                UsersCount = totalUsers,
                ListingsCount = totalListings,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Users()
        {
            var users = await this.usersService.GetAllAsync<UserViewModel>();
            var viewModel = new UsersViewModel
            {
                Users = users,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.usersService.GetUserInfoByIdAsync<UserDetailsViewModel>(id);
            viewModel.Listings = await this.listingsService.GetAllByCreatorIdAsync<UserDetailsListingViewModel>(id);
            return this.View(viewModel);
        }
    }
}
