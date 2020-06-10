namespace CarMarket.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Fuels;

    using Microsoft.AspNetCore.Mvc;

    public class FuelsSelectListViewComponent : ViewComponent
    {
        private readonly IFuelsService fuelsService;

        public FuelsSelectListViewComponent(IFuelsService fuelsService)
        {
            this.fuelsService = fuelsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var fuels = await this.fuelsService.GetAllAsync<FuelSelectListViewModel>();
            var viewModel = new AllFuelsSelectListViewModel
            {
                Fuels = fuels,
                SelectedFuelId = id,
            };

            return this.View(viewModel);
        }
    }
}
