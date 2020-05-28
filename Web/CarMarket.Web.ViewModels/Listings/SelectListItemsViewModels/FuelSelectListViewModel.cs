namespace CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class FuelSelectListViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public SelectListItem FuelSelectListItem => new SelectListItem(this.Type, this.Id.ToString());
    }
}
