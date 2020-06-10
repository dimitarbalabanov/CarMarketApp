namespace CarMarket.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Conditions;

    using Microsoft.AspNetCore.Mvc;

    public class ConditionsSelectListViewComponent : ViewComponent
    {
        private readonly IConditionsService conditionsService;

        public ConditionsSelectListViewComponent(IConditionsService conditionsService)
        {
            this.conditionsService = conditionsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var conditions = await this.conditionsService.GetAllAsync<ConditionSelectListViewModel>();
            var viewModel = new AllConditionsSelectListViewModel
            {
                Conditions = conditions,
                SelectedConditionId = id,
            };

            return this.View(viewModel);
        }
    }
}
