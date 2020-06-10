namespace CarMarket.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Bodies;

    using Microsoft.AspNetCore.Mvc;

    public class BodiesSelectListViewComponent : ViewComponent
    {
        private readonly IBodiesService bodiesService;

        public BodiesSelectListViewComponent(IBodiesService bodiesService)
        {
            this.bodiesService = bodiesService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var bodies = await this.bodiesService.GetAllAsync<BodySelectListViewModel>();
            var viewModel = new AllBodiesSelectListViewModel
            {
                Bodies = bodies,
                SelectedBodyId = id,
            };

            return this.View(viewModel);
        }
    }
}
