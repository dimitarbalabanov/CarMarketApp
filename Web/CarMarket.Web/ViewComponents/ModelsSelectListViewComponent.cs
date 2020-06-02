namespace CarMarket.Web.ViewComponents
{
    using System.Threading.Tasks;
    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class ModelsSelectListViewComponent : ViewComponent
    {
        private readonly IModelsService modelsService;

        public ModelsSelectListViewComponent(IModelsService modelsService)
        {
            this.modelsService = modelsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int makeId)
        {
            var models = await this.modelsService.GetAllByMakeIdAsync<ModelSelectListViewModel>(makeId);
            return this.View(models);
        }
    }
}
