namespace CarMarket.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Models;

    using Microsoft.AspNetCore.Mvc;

    public class ModelsSelectListViewComponent : ViewComponent
    {
        private readonly IModelsService modelsService;

        public ModelsSelectListViewComponent(IModelsService modelsService)
        {
            this.modelsService = modelsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int makeId, int modelId)
        {
            var models = await this.modelsService.GetAllByMakeIdAsync<ModelSelectListViewModel>(makeId);
            var viewModel = new AllModelsSelectListViewModel
            {
                Models = models,
                SelectedModelId = modelId,
            };

            return this.View(viewModel);
        }
    }
}
