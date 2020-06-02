namespace CarMarket.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class BodiesSelectListViewComponent : ViewComponent
    {
        private readonly IBodiesService bodiesService;

        public BodiesSelectListViewComponent(IBodiesService bodiesService)
        {
            this.bodiesService = bodiesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var bodies = await this.bodiesService.GetAllAsync<BodySelectListViewModel>();
            return this.View(bodies);
        }
    }
}
