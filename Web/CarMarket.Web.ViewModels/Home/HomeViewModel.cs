namespace CarMarket.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class HomeViewModel
    {
        public IEnumerable<HomeListingViewModel> Listings { get; set; }

        public int? MakeId { get; set; }

        public int? ModelId { get; set; }

        public decimal? PriceTo { get; set; }

        public int OrderingValue { get; set; }
    }
}
