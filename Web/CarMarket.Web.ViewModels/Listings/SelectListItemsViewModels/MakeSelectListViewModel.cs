using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels
{
    public class MakeSelectListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SelectListItem MakeSelectListItem => new SelectListItem(this.Name, this.Id.ToString());
    }
}
