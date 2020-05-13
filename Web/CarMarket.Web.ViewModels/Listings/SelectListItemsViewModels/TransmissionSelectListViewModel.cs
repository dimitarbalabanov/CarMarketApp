using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels
{
    public class TransmissionSelectListViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public SelectListItem TransmissionSelectListItem => new SelectListItem(this.Type, this.Id.ToString());
    }
}
