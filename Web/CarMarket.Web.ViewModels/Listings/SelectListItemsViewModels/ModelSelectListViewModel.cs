namespace CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ModelSelectListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SelectListItem ModelSelectListItem => new SelectListItem(this.Name, this.Id.ToString());
    }
}
