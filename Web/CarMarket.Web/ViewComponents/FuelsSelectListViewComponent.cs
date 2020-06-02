namespace CarMarket.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class FuelsSelectListViewComponent : ViewComponent
    {
        private readonly IFuelsService fuelsService;

        public FuelsSelectListViewComponent(IFuelsService fuelsService)
        {
            this.fuelsService = fuelsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var fuels = await this.fuelsService.GetAllAsync<FuelSelectListViewModel>();
            return this.View(fuels);
        }
    }
}
