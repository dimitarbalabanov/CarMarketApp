namespace CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ColorSelectListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SelectListItem ColorSelectListItem => new SelectListItem(this.Name, this.Id.ToString());
    }
}
