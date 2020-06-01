namespace CarMarket.Web.ViewModels.Search
{
    using System.Collections.Generic;

    public class SearchResultViewModel
    {
        public IEnumerable<SearchResultListingViewModel> Listings { get; set; }
    }
}
