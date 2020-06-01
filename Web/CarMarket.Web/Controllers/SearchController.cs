namespace CarMarket.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Services.Data.SearchServiceHelpers.Dtos;
    using CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels;
    using CarMarket.Web.ViewModels.Search;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class SearchController : Controller
    {
        private readonly IBodiesService bodiesService;
        private readonly IColorsService colorsService;
        private readonly IConditionsService conditionsService;
        private readonly IFuelsService fuelsService;
        private readonly IListingsService listingsService;
        private readonly IMakesService makesService;
        private readonly ITransmissionsService transmissionsService;
        private readonly IModelsService modelsService;
        private readonly ISearchService searchService;
        private readonly IMapper mapper;

        public SearchController(
            IBodiesService bodiesService,
            IColorsService colorsService,
            IConditionsService conditionsService,
            IFuelsService fuelsService,
            IListingsService listingsService,
            IMakesService makesService,
            ITransmissionsService transmissionsService,
            IModelsService modelsService,
            ISearchService searchService,
            IMapper mapper)
        {
            this.bodiesService = bodiesService;
            this.colorsService = colorsService;
            this.conditionsService = conditionsService;
            this.fuelsService = fuelsService;
            this.listingsService = listingsService;
            this.makesService = makesService;
            this.transmissionsService = transmissionsService;
            this.modelsService = modelsService;
            this.searchService = searchService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var bodies = await this.bodiesService.GetAllAsync<BodySelectListViewModel>();
            var colors = await this.colorsService.GetAllAsync<ColorSelectListViewModel>();
            var conditions = await this.conditionsService.GetAllAsync<ConditionSelectListViewModel>();
            var fuels = await this.fuelsService.GetAllAsync<FuelSelectListViewModel>();
            var makes = await this.makesService.GetAllAsync<MakeSelectListViewModel>();
            var transmissions = await this.transmissionsService.GetAllAsync<TransmissionSelectListViewModel>();
            var orderingValues = this.searchService.GetOrderingValues;

            var viewModel = new SearchInputModel
            {
                Bodies = bodies.Select(x => x.BodySelectListItem),
                Colors = colors.Select(x => x.ColorSelectListItem),
                Conditions = conditions.Select(x => x.ConditionSelectListItem),
                Fuels = fuels.Select(x => x.FuelSelectListItem),
                Makes = makes.Select(x => x.MakeSelectListItem),
                Transmissions = transmissions.Select(x => x.TransmissionSelectListItem),
                OrderingValues = orderingValues.Select(x => new SelectListItem(x.Value, x.Key.ToString())),
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
