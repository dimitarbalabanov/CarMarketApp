using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels
{
    public class ColorSelectListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SelectListItem ColorSelectListItem => new SelectListItem(this.Name, this.Id.ToString());
    }
}
