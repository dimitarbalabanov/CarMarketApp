namespace CarMarket.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Listings;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ListingsController : Controller
    {
        private readonly IBodiesService bodiesService;
        private readonly IColorsService colorsService;
        private readonly IConditionsService conditionsService;
        private readonly IFuelsService fuelsService;
        private readonly IListingsService listingsService;
        private readonly IMakesService makesService;
        private readonly ITransmissionsService transmissionsService;

        public ListingsController(
            IBodiesService bodiesService,
            IColorsService colorsService,
            IConditionsService conditionsService,
            IFuelsService fuelsService,
            IListingsService listingsService,
            IMakesService makesService,
            ITransmissionsService transmissionsService)
        {
            this.bodiesService = bodiesService;
            this.colorsService = colorsService;
            this.conditionsService = conditionsService;
            this.fuelsService = fuelsService;
            this.listingsService = listingsService;
            this.makesService = makesService;
            this.transmissionsService = transmissionsService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var bodies = this.bodiesService.GetAll<BodyDropDownViewModel>();
            var colors = this.colorsService.GetAll<ColorDropDownViewModel>();
            var conditions = this.conditionsService.GetAll<ConditionDropDownViewModel>();
            var fuels = this.fuelsService.GetAll<FuelDropDownViewModel>();
            var makes = this.makesService.GetAll<MakeDropDownViewModel>();
            var transmissions = this.transmissionsService.GetAll<TransmissionDropDownViewModel>();

            var viewModel = new CreateListingInputModel
            {
                Bodies = bodies,
                Colors = colors,
                Conditions = conditions,
                Fuels = fuels,
                Makes = makes,
                Transmissions = transmissions,
            };
            return this.View(viewModel);
        }

        public IActionResult Details(int? id = null)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateListingInputModel input)
        {
            Console.WriteLine();
            return null;
        }
    }
}
