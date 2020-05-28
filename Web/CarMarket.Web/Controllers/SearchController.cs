namespace CarMarket.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels;
    using CarMarket.Web.ViewModels.Search;

    using Microsoft.AspNetCore.Mvc;

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

        public SearchController(
            IBodiesService bodiesService,
            IColorsService colorsService,
            IConditionsService conditionsService,
            IFuelsService fuelsService,
            IListingsService listingsService,
            IMakesService makesService,
            ITransmissionsService transmissionsService,
            IModelsService modelsService,
            ISearchService searchService)
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
        }

        public async Task<IActionResult> Index()
        {
            var bodies = await this.bodiesService.GetAllAsync<BodySelectListViewModel>();
            var colors = await this.colorsService.GetAllAsync<ColorSelectListViewModel>();
            var conditions = await this.conditionsService.GetAllAsync<ConditionSelectListViewModel>();
            var fuels = await this.fuelsService.GetAllAsync<FuelSelectListViewModel>();
            var makes = await this.makesService.GetAllAsync<MakeSelectListViewModel>();
            var transmissions = await this.transmissionsService.GetAllAsync<TransmissionSelectListViewModel>();

            var viewModel = new SearchInputModel
            {
                Bodies = bodies.Select(x => x.BodySelectListItem),
                Colors = colors.Select(x => x.ColorSelectListItem),
                Conditions = conditions.Select(x => x.ConditionSelectListItem),
                Fuels = fuels.Select(x => x.FuelSelectListItem),
                Makes = makes.Select(x => x.MakeSelectListItem),
                Transmissions = transmissions.Select(x => x.TransmissionSelectListItem),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Search()
        {
            return null;
        }
    }
}
