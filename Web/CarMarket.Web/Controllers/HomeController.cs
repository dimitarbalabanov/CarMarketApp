namespace CarMarket.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels;
    using CarMarket.Web.ViewModels.Home;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly IListingsService listingsService;

        public HomeController(IListingsService listingsService)
        {
            this.listingsService = listingsService;
        }

        public async Task<IActionResult> Index()
        {
            var listings = await this.listingsService.GetLatestAsync<HomeListingViewModel>();
            var viewModel = new HomeViewModel { Listings = listings };
            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
