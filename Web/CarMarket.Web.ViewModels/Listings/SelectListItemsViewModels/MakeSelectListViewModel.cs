namespace CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class MakeSelectListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SelectListItem MakeSelectListItem => new SelectListItem(this.Name, this.Id.ToString());
    }
}
