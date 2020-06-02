namespace CarMarket.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Services.Data.SearchServiceHelpers.Dtos;
    using CarMarket.Web.ViewModels.Search;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class SearchController : Controller
    {
        private readonly ISearchService searchService;
        private readonly IMapper mapper;

        public SearchController(ISearchService searchService, IMapper mapper)
        {
            this.searchService = searchService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var viewModel = new SearchInputModel
            {
                OrderingValues = this.searchService.GetOrderingValues.Select(x => new SelectListItem(x.Value, x.Key.ToString())),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Result(SearchInputModel searchInput)
        {
            var searchModel = this.mapper.Map<SearchModelDto>(searchInput);
            var listings = await this.searchService.GetSearchResultAsync<SearchResultListingViewModel>(searchModel);
            var viewModel = new SearchResultViewModel { Listings = listings };
            return this.View(viewModel);
        }
    }
}
