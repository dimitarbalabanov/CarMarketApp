using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels
{
    public class ConditionSelectListViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public SelectListItem ConditionSelectListItem => new SelectListItem(this.Type, this.Id.ToString());
    }
}
