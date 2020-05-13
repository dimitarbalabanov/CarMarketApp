using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels
{
    public class FuelSelectListViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public SelectListItem FuelSelectListItem => new SelectListItem(this.Type, this.Id.ToString());
    }
}
