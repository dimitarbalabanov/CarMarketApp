using CarMarket.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMarket.Web.ViewComponents
{
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
