namespace CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ConditionSelectListViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public SelectListItem ConditionSelectListItem => new SelectListItem(this.Type, this.Id.ToString());
    }
}
