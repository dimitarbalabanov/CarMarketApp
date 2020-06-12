namespace CarMarket.Web.ViewModels.Search
{
    using CarMarket.Services.Data.Pagination;

    public class SearchResultViewModel
    {
        public PaginatedList<SearchResultListingViewModel> Listings { get; set; }
    }
}
