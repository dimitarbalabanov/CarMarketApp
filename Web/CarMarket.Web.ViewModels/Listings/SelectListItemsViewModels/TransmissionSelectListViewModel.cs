namespace CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class TransmissionSelectListViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public SelectListItem TransmissionSelectListItem => new SelectListItem(this.Type, this.Id.ToString());
    }
}
