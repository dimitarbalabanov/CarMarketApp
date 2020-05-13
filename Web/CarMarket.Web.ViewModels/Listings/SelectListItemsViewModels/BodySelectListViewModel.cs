using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels
{
    public class BodySelectListViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public SelectListItem BodySelectListItem => new SelectListItem(this.Type, this.Id.ToString());
    }
}
