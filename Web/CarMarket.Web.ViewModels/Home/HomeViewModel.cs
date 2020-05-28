namespace CarMarket.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class HomeViewModel
    {
        public IEnumerable<HomeListingViewModel> Listings { get; set; }
    }
}
