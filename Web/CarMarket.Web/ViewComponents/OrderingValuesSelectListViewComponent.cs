namespace CarMarket.Web.ViewComponents
{
    using CarMarket.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class OrderingValuesSelectListViewComponent : ViewComponent
    {
        private readonly ISearchService searchService;

        public OrderingValuesSelectListViewComponent(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public IViewComponentResult Invoke()
        {
            var orderingValues = this.searchService.GetOrderingValues;
            return this.View(orderingValues);
        }
    }
}
