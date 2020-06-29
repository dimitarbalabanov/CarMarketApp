namespace CarMarket.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Dtos;
    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Search;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : Controller
    {
        private const int DefaultPageNumber = 1;

        private readonly ISearchService searchService;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public SearchController(
            ISearchService searchService,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.searchService = searchService;
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
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

            string userId = null;
            if (this.signInManager.IsSignedIn(this.User))
            {
                userId = this.userManager.GetUserId(this.User);
            }

            var listings = await this.searchService
                .GetSearchResultAsync<SearchResultListingViewModel>(searchModel, userId, pageNumber ?? DefaultPageNumber);

            var viewModel = new SearchResultViewModel { Listings = listings };
            return this.View(viewModel);
        }
    }
}
