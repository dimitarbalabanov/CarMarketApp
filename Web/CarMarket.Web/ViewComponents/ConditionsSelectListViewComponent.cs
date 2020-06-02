namespace CarMarket.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class ConditionsSelectListViewComponent : ViewComponent
    {
        private readonly IConditionsService conditionsService;

        public ConditionsSelectListViewComponent(IConditionsService conditionsService)
        {
            this.conditionsService = conditionsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var conditions = await this.conditionsService.GetAllAsync<ConditionSelectListViewModel>();
            return this.View(conditions);
        }
    }
}
