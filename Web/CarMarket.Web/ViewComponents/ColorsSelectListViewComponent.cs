namespace CarMarket.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Colors;

    using Microsoft.AspNetCore.Mvc;

    public class ColorsSelectListViewComponent : ViewComponent
    {
        private readonly IColorsService colorsService;

        public ColorsSelectListViewComponent(IColorsService colorsService)
        {
            this.colorsService = colorsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var colors = await this.colorsService.GetAllAsync<ColorSelectListViewModel>();
            var viewModel = new AllColorsSelectListViewModel
            {
                Colors = colors,
                SelectedColorId = id,
            };

            return this.View(viewModel);
        }
    }
}
