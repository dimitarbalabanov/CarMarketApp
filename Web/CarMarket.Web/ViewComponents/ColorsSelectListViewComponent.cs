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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var colors = await this.colorsService.GetAllAsync<ColorSelectListViewModel>();
            return this.View(colors);
        }
    }
}
