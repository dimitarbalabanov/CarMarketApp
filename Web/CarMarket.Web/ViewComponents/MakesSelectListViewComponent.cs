namespace CarMarket.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Makes;

    using Microsoft.AspNetCore.Mvc;

    public class MakesSelectListViewComponent : ViewComponent
    {
        private readonly IMakesService makesService;

        public MakesSelectListViewComponent(IMakesService makesService)
        {
            this.makesService = makesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var makes = await this.makesService.GetAllAsync<MakeSelectListViewModel>();
            return this.View(makes);
        }
    }
}
