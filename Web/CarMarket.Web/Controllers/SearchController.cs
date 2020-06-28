namespace CarMarket.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Services.Data.Dtos;
    using CarMarket.Web.ViewModels.Search;

    using Microsoft.AspNetCore.Mvc;

    public class SearchController : Controller
    {
        private const int DefaultPageNumber = 1;

        private readonly ISearchService searchService;
        private readonly IMapper mapper;

        public SearchController(ISearchService searchService, IMapper mapper)
        {
            this.searchService = searchService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var viewModel = new SearchInputModel();
            return this.View(viewModel);
        }

        public async Task<IActionResult> Result(SearchInputModel searchInput, int? pageNumber)
        {
            var queryValuesDictionary = this.Request.Query.ToDictionary(x => x.Key, y => y.Value.ToString());
            this.ViewData["Query"] = queryValuesDictionary;
            var searchModel = this.mapper.Map<SearchModelDto>(searchInput);

            var listings = await this.searchService
                .GetSearchResultAsync<SearchResultListingViewModel>(searchModel, pageNumber ?? DefaultPageNumber);

            var viewModel = new SearchResultViewModel { Listings = listings };
            return this.View(viewModel);
        }
    }
}
